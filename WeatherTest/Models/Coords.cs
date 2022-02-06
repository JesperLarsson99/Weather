using WeatherTest.Contracts;

namespace WeatherTest.Models
{
    public class Coords : ICoords
    {
        public float longitude { get; set; }
        public float latitude { get; set; }
    }
}
