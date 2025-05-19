namespace MyApp.Models
{
    public class UserPreferences
    {
        public int UserPreferencesId { get; set; }
        public int OrderId { get; set; }
        public string PreferencesJson { get; set; } // This will store the configuration as JSON
    }
}
