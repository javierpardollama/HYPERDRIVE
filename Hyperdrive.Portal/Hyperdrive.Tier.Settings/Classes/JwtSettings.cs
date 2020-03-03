namespace Hyperdrive.Tier.Settings.Classes
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }

        public double JwtExpireDays { get; set; }
    }
}
