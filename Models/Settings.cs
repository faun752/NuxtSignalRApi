namespace NuxtSignalRApi.Models
{
    public class Settings
    {
        public bool UseSignalRBackplane { get; set; } = false;
        public string RedisConnectionString { get; set; }
    }
}