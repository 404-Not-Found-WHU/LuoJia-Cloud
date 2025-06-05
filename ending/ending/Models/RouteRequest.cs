using System.ComponentModel.DataAnnotations;

namespace ending.Models
{
    public class RouteRequest
    {
        [Required(ErrorMessage = "必须提供开始时间")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "时间格式应为HH:mm")]
        [Display(Name = "开始时间")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "必须提供结束时间")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "时间格式应为HH:mm")]
        [Display(Name = "结束时间")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "必须选择兴趣类型")]
        [Display(Name = "兴趣类型")]
        public string InterestType { get; set; }
        
        /// <summary>
        /// 用户当前位置的纬度
        /// </summary>
        public double? CurrentLatitude { get; set; }
        
        /// <summary>
        /// 用户当前位置的经度
        /// </summary>
        public double? CurrentLongitude { get; set; }
    }
}