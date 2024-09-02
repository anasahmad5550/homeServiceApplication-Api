namespace HSA.Common.Utilities
{
    public class AppConfig
    {
        public string? DbConnectionString { get; set; }
        public JWT? jwt { get; set; }
    }

    public class JWT
    {
        public string? key { get; set; }
        public string? Issuer { get; set; }
    }
}
