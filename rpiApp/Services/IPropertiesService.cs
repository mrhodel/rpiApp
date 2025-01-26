/*
 * IPorpertiesService.cs
 * 1/26/2025 Mike Hodel
*/
using rpiApp.Models;

namespace rpiApp.Services
{
    public interface IPropertiesService
    {
        Properties Properties { get; set; }
    }

    public class PropertiesService : IPropertiesService
    {
        public Properties Properties { get; set; } = new Properties("Application Properties");
    }
}
