namespace ending.Models
{
    public class RouteScenery
    {
        public string RouteID { get; set; }
        public string SceneryID { get; set; }
        public int OrderIndex { get; set; }
        public string TimeWindow { get; set; }
        
        public Scenery Scenery { get; set; }
    }
}