namespace ending.Models
{
    public class RouteResponse
    {
        public List<RouteScenery> Sceneries { get; set; } = new();
        public int TotalAttractions { get; set; }
        public string TimeConsumption { get; set; }
        public string RouteSummary { get; set; }
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
        public string StaticMapUrl { get; set; }
    }
}