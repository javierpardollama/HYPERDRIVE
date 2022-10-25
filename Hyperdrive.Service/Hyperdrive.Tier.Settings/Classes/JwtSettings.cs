using System.Collections.Generic;

namespace Hyperdrive.Tier.Settings.Classes
{
    /// <summary>
    /// Represents a <see cref="JwtSettings"/> class
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or Sets <see cref="JwtKey"/>
        /// </summary>
        public string JwtKey { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtIssuer"/>
        /// </summary>
        public string JwtIssuer { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtAudiences"/>
        /// </summary>
        public IList<string> JwtAudiences { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtExpireMinutes"/>
        /// </summary>
        public double JwtExpireMinutes { get; set; }
    }
}
