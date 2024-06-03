using System.ComponentModel.DataAnnotations;
namespace LiveBlip.Models;
public class Device
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
