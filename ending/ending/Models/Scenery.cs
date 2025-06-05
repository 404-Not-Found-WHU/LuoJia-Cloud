namespace ending.Models
{
    public class Scenery
    {
        public string SceneryID { get; set; }
        public string SceneryName { get; set; }
        public string Address { get; set; }
        public string OpenTime { get; set; }
        public string SceneryType { get; set; }
        public double HumanDensity { get; set; }
        public int PlayingTime { get; set; }
        public string Zone { get; set; }
        
        public TimeSpan OpenStart { get; set; }
        public TimeSpan OpenEnd { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
}
}