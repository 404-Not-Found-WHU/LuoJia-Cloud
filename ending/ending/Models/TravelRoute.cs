namespace ending.Models
{
    public class TravelRoute
    {
        public string RouteID { get; set; }
        public string RouteName { get; set; }
        public int TotalTime { get; set; }
        public List<RouteScenery> RouteSceneries { get; set; } = new();
    }
}