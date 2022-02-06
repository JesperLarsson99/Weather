using System.ComponentModel.DataAnnotations;

namespace WeatherTest.Contracts
{
    public interface ICoords
    {
        float longitude { get; set; }
        float latitude { get; set; }
    }
}
