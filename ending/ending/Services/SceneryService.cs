using ending.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ending.Services
{
    public class SceneryService
    {
        private readonly List<Scenery> _sceneries;
        private const int TransferTime = 15; // 景点间移动时间（分钟）

        public SceneryService()
        {
            _sceneries = GetCompleteSceneryData();
        }

        public RouteResponse RecommendRoutes(RouteRequest request)
        {
            // 参数验证
            var (startTime, endTime) = (ParseTime(request.StartTime), ParseTime(request.EndTime));
            var totalMinutes = (int)(endTime - startTime).TotalMinutes;

            if (totalMinutes <= 0)
                return CreateErrorResponse("结束时间必须晚于开始时间");

            // 动态计算推荐数量范围
            var (minCount, maxCount) = CalculateAttractionCount(totalMinutes);
            if (totalMinutes < minCount * 30)
                return CreateErrorResponse($"至少需要 {Math.Ceiling(minCount * 0.5)} 小时游览时间");

            // 生成路线，考虑用户当前位置
            var routeSceneries = BuildOptimizedRoute(startTime, endTime, request.InterestType, minCount, maxCount, request.CurrentLatitude, request.CurrentLongitude);
            return FormatSuccessResponse(routeSceneries, startTime, endTime);
        }

        private List<RouteScenery> BuildOptimizedRoute(TimeSpan start, TimeSpan end, string interestType, int minCount, int maxCount, double? userLatitude, double? userLongitude)
        {
            var currentTime = start;
            var route = new List<RouteScenery>();
            
            // 获取候选景点，考虑用户当前位置
            var candidates = GetCandidates(interestType, start, end, maxCount, userLatitude, userLongitude);

            foreach (var scenery in candidates)
            {
                // 动态时间分配算法
                var (visitDuration, moveTime) = CalculateDynamicTime(scenery, currentTime, end, route.Count, minCount, route);

                if (visitDuration <= 0 || route.Count >= maxCount)
                    break;

                var visitEnd = currentTime.Add(TimeSpan.FromMinutes(visitDuration));
                if (visitEnd > scenery.OpenEnd || visitEnd > end)
                    continue;

                route.Add(CreateRouteScenery(scenery, currentTime, visitEnd, visitDuration, route.Count + 1));
                currentTime = visitEnd.Add(TimeSpan.FromMinutes(moveTime));
            }

            // 保障最低数量要求
            return EnsureMinimumAttractions(route, start, end, minCount, userLatitude, userLongitude);
        }

        #region 核心算法实现
        private (int visitDuration, int moveTime) CalculateDynamicTime(Scenery scenery, TimeSpan current, TimeSpan end, int currentCount, int minCount, List<RouteScenery> route)
        {
            var remainingTime = (int)(end - current).TotalMinutes;
            var remainingSpots = minCount - currentCount;

            // 优先保证最低数量
            var baseDuration = remainingSpots > 0
                ? Math.Min(scenery.PlayingTime, Math.Max(30, remainingTime / remainingSpots - TransferTime))
                : Math.Min(scenery.PlayingTime, remainingTime - TransferTime);

            // 移动时间优化
            var moveTime = currentCount == 0 ? 0 :
                (route.Last().Scenery.Zone == scenery.Zone ? 5 : TransferTime);

            return (baseDuration, moveTime);
        }

        private List<Scenery> GetCandidates(string interestType, TimeSpan start, TimeSpan end, int maxCount, double? userLatitude, double? userLongitude)
        {
            var candidates = _sceneries
                .Where(s => s.SceneryType == interestType)
                .Where(s => s.OpenStart < end && s.OpenEnd > start)
                .ToList();
                
            // 如果用户提供了当前位置，则按照距离排序
            if (userLatitude.HasValue && userLongitude.HasValue)
            {
                // 计算每个景点到用户当前位置的距离
                var sceneryWithDistance = candidates.Select(s => new
                {
                    Scenery = s,
                    Distance = CalculateDistance(userLatitude.Value, userLongitude.Value, s.Latitude, s.Longitude)
                });
                
                // 按距离排序，优先推荐距离近的景点
                return sceneryWithDistance
                    .OrderBy(s => s.Distance) // 首先按距离排序
                    .ThenBy(s => s.Scenery.HumanDensity) // 然后按人流密度排序
                    .ThenByDescending(s => s.Scenery.PlayingTime) // 最后按游玩时间排序
                    .Select(s => s.Scenery)
                    .Take(maxCount * 3) // 获取足够候选
                    .ToList();
            }
            else
            {
                // 如果没有用户位置，则使用原来的排序方式
                return candidates
                    .OrderBy(s => s.HumanDensity) // 按人流密度排序
                    .ThenByDescending(s => s.PlayingTime)
                    .Take(maxCount * 3) // 获取足够候选
                    .ToList();
            }
        }
        
        /// <summary>
        /// 计算两个经纬度坐标之间的距离（使用Haversine公式）
        /// </summary>
        /// <param name="lat1">第一个点的纬度</param>
        /// <param name="lon1">第一个点的经度</param>
        /// <param name="lat2">第二个点的纬度</param>
        /// <param name="lon2">第二个点的经度</param>
        /// <returns>距离（单位：公里）</returns>
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadius = 6371; // 地球半径，单位：公里
            
            // 将经纬度转换为弧度
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            
            // Haversine公式
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadius * c;
            
            return distance;
        }
        
        /// <summary>
        /// 将角度转换为弧度
        /// </summary>
        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private List<RouteScenery> EnsureMinimumAttractions(List<RouteScenery> route, TimeSpan start, TimeSpan end, int minCount, double? userLatitude, double? userLongitude)
        {
            if (route.Count >= minCount)
                return route.Take(10).ToList();

            // 补充快速游览景点
            var backupCandidates = _sceneries
                .Where(s => s.PlayingTime <= 30)
                .ToList();
                
            // 如果用户提供了当前位置，则按照距离排序
            List<Scenery> backupList;
            if (userLatitude.HasValue && userLongitude.HasValue)
            {
                // 计算每个景点到用户当前位置的距离
                var sceneryWithDistance = backupCandidates.Select(s => new
                {
                    Scenery = s,
                    Distance = CalculateDistance(userLatitude.Value, userLongitude.Value, s.Latitude, s.Longitude)
                });
                
                // 按距离排序，优先推荐距离近的景点
                backupList = sceneryWithDistance
                    .OrderBy(s => s.Distance) // 首先按距离排序
                    .ThenBy(s => s.Scenery.HumanDensity) // 然后按人流密度排序
                    .Select(s => s.Scenery)
                    .Take(15)
                    .ToList();
            }
            else
            {
                // 如果没有用户位置，则使用原来的排序方式
                backupList = backupCandidates
                    .OrderBy(s => s.HumanDensity)
                    .Take(15)
                    .ToList();
            }

            var current = route.LastOrDefault()?.Scenery.OpenEnd ?? start;
            var optimized = new List<RouteScenery>(route);
            var orderIndex = route.Count + 1;

            foreach (var scenery in backupList)
            {
                if (optimized.Count >= minCount) break;

                var duration = Math.Min(30, (int)(end - current).TotalMinutes - TransferTime);
                if (duration < 15) break;

                var visitEnd = current.Add(TimeSpan.FromMinutes(duration));
                optimized.Add(CreateRouteScenery(
                    scenery,
                    current,
                    visitEnd,
                    duration,
                    orderIndex++
                ));

                current = visitEnd.Add(TimeSpan.FromMinutes(TransferTime));
            }

            return optimized.Take(10).ToList();
        }
        #endregion

        #region 辅助方法
        private (int min, int max) CalculateAttractionCount(int totalMinutes)
        {
            return totalMinutes switch
            {
                <= 120 => (3, 5),   // 2小时以下
                <= 240 => (5, 8),   // 2-4小时
                <= 360 => (7, 10),  // 4-6小时
                _ => (8, 10)   // 6小时+
            };
        }

        private RouteScenery CreateRouteScenery(Scenery scenery, TimeSpan start, TimeSpan end, int duration, int orderIndex)
        {
            return new RouteScenery
            {
                SceneryID = scenery.SceneryID,
                OrderIndex = orderIndex,
                TimeWindow = $"{start:hh\\:mm}-{end:hh\\:mm}",
                Scenery = scenery
            };
        }

        private RouteResponse FormatSuccessResponse(List<RouteScenery> sceneries, TimeSpan start, TimeSpan end)
        {
            var totalDuration = (int)(end - start).TotalMinutes;
            var usedTime = sceneries.Sum(s => s.Scenery.PlayingTime) + (sceneries.Count > 1 ? (sceneries.Count - 1) * TransferTime : 0);
            
            var response = new RouteResponse
            {
                Sceneries = sceneries,
                TotalAttractions = sceneries.Count,
                TimeConsumption = $"{usedTime}分钟（含移动时间）",
                RouteSummary = GenerateRouteSummary(sceneries),
                Success = true
            };
            
            return response;
        }

        private string GenerateRouteSummary(List<RouteScenery> sceneries)
        {
            if (!sceneries.Any()) return "未找到符合条件的路线";
            
            var zones = sceneries.Select(n => n.Scenery.Zone).Distinct();
            var type = sceneries.First().Scenery.SceneryType;
            return $"途经 {string.Join(" → ", zones)} 区域，包含 {type} 类型景点";
        }
        
        private TimeSpan ParseTime(string input) => TimeSpan.Parse(input.Replace('：', ':'));

        private RouteResponse CreateErrorResponse(string message) => new()
        {
            Sceneries = new List<RouteScenery>(),
            RouteSummary = $"错误：{message}",
            Success = false,
            ErrorMessage = message
        };
        #endregion

        #region 完整景点数据集
        private List<Scenery> GetCompleteSceneryData()
        {
            var sceneries = new List<Scenery>
            {
                // =============== 人文景观 ===============
            
                new Scenery{ SceneryID="C01", SceneryName="老斋舍", OpenTime="08:00-18:00",
                           PlayingTime=45, HumanDensity=0.2, Zone="中心区", SceneryType="人文",
                           Longitude=114.363891, Latitude=30.539614 },
                new Scenery{ SceneryID="C02", SceneryName="万林艺术博物馆", OpenTime="09:00-17:00",
                           PlayingTime=60, HumanDensity=0.5, Zone="中心区", SceneryType="人文",
                           Longitude=114.363073, Latitude=30.536759 },
                new Scenery{ SceneryID="C03", SceneryName="总图书馆（文理学部）", OpenTime="08:30-17:30",
                           PlayingTime=45, HumanDensity=0.3, Zone="中心区", SceneryType="人文",
                           Longitude=114.369302, Latitude=30.537987 },
                new Scenery{ SceneryID="C04", SceneryName="九一二操场纪念牌坊", OpenTime="06:00-22:00",
                           PlayingTime=20, HumanDensity=0.1, Zone="中心区", SceneryType="人文",
                           Longitude=114.366465, Latitude=30.538735 },
                new Scenery{ SceneryID="C05", SceneryName="李达雕像", OpenTime="00:00-23:59",
                           PlayingTime=15, HumanDensity=0.1, Zone="中心区", SceneryType="人文",
                           Longitude=114.36434, Latitude=30.53727 },
                new Scenery{ SceneryID="C06", SceneryName="宋卿体育馆", OpenTime="07:00-22:00",
                           PlayingTime=30, HumanDensity=0.4, Zone="中心区", SceneryType="人文",
                           Longitude=114.361704, Latitude=30.538244 },
                new Scenery{ SceneryID="C07", SceneryName="周恩来故居", OpenTime="09:00-17:00",
                           PlayingTime=40, HumanDensity=0.6, Zone="中心区", SceneryType="人文",
                           Longitude=114.372264, Latitude=30.53451 },

                new Scenery{ SceneryID="C08", SceneryName="半山庐历史建筑", OpenTime="08:00-18:00",
                           PlayingTime=30, HumanDensity=0.2, Zone="中心区", SceneryType="人文",
                           Longitude=114.366798, Latitude=30.535579 },
                new Scenery{ SceneryID="C09", SceneryName="鲲鹏广场纪念碑", OpenTime="06:00-21:00",
                           PlayingTime=20, HumanDensity=0.3, Zone="中心区", SceneryType="人文",
                           Longitude=114.362168, Latitude=30.538599 },

                new Scenery{ SceneryID="C10", SceneryName="武汉大学早期建筑工学院", OpenTime="08:00-18:00",
                           PlayingTime=30, HumanDensity=0.2, Zone="中心区", SceneryType="人文",
                           Longitude=114.366488, Latitude=30.537508 },

                new Scenery{ SceneryID="C11", SceneryName="武汉大学早期建筑", OpenTime="08:00-18:00",
                           PlayingTime=30, HumanDensity=0.2, Zone="中心区", SceneryType="人文",
                           Longitude=114.36386, Latitude=30.540156 },

                // 北区（4个）
                new Scenery{ SceneryID="C12", SceneryName="六一纪念亭", OpenTime="00:00-23:59",
                           PlayingTime=25, HumanDensity=0.1, Zone="北区", SceneryType="人文",
                           Longitude=114.361451, Latitude=30.536949 },

                new Scenery{ SceneryID="C13", SceneryName=" 张之洞塑像", OpenTime="09:00-17:00",
                           PlayingTime=45, HumanDensity=0.3, Zone="北区", SceneryType="人文",
                           Longitude=114.372341, Latitude=30.539337 },
                new Scenery{ SceneryID="C14", SceneryName="珞珈山水塔", OpenTime="08:00-18:00",
                           PlayingTime=20, HumanDensity=0.1, Zone="北区", SceneryType="人文",
                           Longitude=114.372354, Latitude=30.534958 },

                new Scenery{ SceneryID="C15", SceneryName="闻一多塑像", OpenTime="08:00-18:00",
                           PlayingTime=20, HumanDensity=0.1, Zone="北区", SceneryType="人文",
                           Longitude=114.365089, Latitude=30.540306 },

                // 东区（4个）

                new Scenery{ SceneryID="C16", SceneryName="李四光雕像", OpenTime="00:00-23:59",
                           PlayingTime=15, HumanDensity=0.1, Zone="东区", SceneryType="人文",
                           Longitude=114.361745, Latitude=30.537402 },

                new Scenery{ SceneryID="C17", SceneryName="武汉大学校史馆", OpenTime="06:30-20:00",
                           PlayingTime=20, HumanDensity=0.3, Zone="东区", SceneryType="人文",
                           Longitude=114.363878, Latitude=30.54012 },



                // =============== 自然景观 ===============

                new Scenery{ SceneryID="N01", SceneryName="武汉大学-樱花城堡    ", OpenTime="00:00-23:59",
                           PlayingTime=45, HumanDensity=0.7, Zone="中心区", SceneryType="自然",
                           Longitude=114.364055, Latitude=30.539602 },

                new Scenery{ SceneryID="N02", SceneryName="武汉大学狮子山", OpenTime="06:00-21:00",
                           PlayingTime=30, HumanDensity=0.4, Zone="中心区", SceneryType="自然",
                           Longitude=114.364072, Latitude=30.540565 },
                new Scenery{ SceneryID="N03", SceneryName="武汉大学落英湖", OpenTime="06:00-20:00",
                           PlayingTime=25, HumanDensity=0.3, Zone="中心区", SceneryType="自然",
                           Longitude=114.363237, Latitude=30.537973 },
                new Scenery{ SceneryID="N04", SceneryName="竹园", OpenTime="06:00-21:00",
                           PlayingTime=20, HumanDensity=0.2, Zone="中心区", SceneryType="自然",
                           Longitude=114.361282, Latitude=30.529081 },

                // 北区
                new Scenery{ SceneryID="N05", SceneryName="珞珈山", OpenTime="05:00-22:00",
                           PlayingTime=120, HumanDensity=0.4, Zone="北区", SceneryType="自然",
                           Longitude=114.360437, Latitude=30.533255 },

                // 东区
                new Scenery{ SceneryID="N06", SceneryName="东湖凌波门", OpenTime="05:00-20:00",
                           PlayingTime=60, HumanDensity=0.7, Zone="东区", SceneryType="自然",
                           Longitude=114.371448, Latitude=30.543311 },


            };

            // 处理开放时间
            foreach (var scenery in sceneries)
            {
                var timeParts = scenery.OpenTime.Split('-');
                if (timeParts.Length == 2)
                {
                    scenery.OpenStart = TimeSpan.Parse(timeParts[0]);
                    scenery.OpenEnd = TimeSpan.Parse(timeParts[1]);
                }
            }

            return sceneries;
        }
        #endregion
    }
}