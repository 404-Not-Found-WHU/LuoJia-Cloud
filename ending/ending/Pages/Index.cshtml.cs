using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ending.Models;
using ending.Services;

namespace ending.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SceneryService _sceneryService;

        [BindProperty]
        public RouteRequest RouteRequest { get; set; }
        
        public RouteResponse RouteResponse { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SceneryService sceneryService)
        {
            _logger = logger;
            _sceneryService = sceneryService;
            RouteRequest = new RouteRequest
            {
                StartTime = "09:00",
                EndTime = "17:00"
            };
        }

        public void OnGet()
        {
            // 初始页面加载，不需要执行任何操作
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _logger.LogInformation("生成路线请求: {StartTime} - {EndTime}, 类型: {Type}", 
                    RouteRequest.StartTime, RouteRequest.EndTime, RouteRequest.InterestType);
                
                RouteResponse = _sceneryService.RecommendRoutes(RouteRequest);
                
                if (!RouteResponse.Success)
                {
                    _logger.LogWarning("路线生成失败: {Error}", RouteResponse.ErrorMessage);
                }
                else
                {
                    _logger.LogInformation("成功生成路线，包含 {Count} 个景点", RouteResponse.TotalAttractions);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成路线时发生错误");
                ModelState.AddModelError(string.Empty, $"生成路线时发生错误: {ex.Message}");
            }

            return Page();
        }
    }
}
