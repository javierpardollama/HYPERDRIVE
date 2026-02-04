using System;
using System.Collections.Generic;
using System.Linq;

namespace Hyperdrive.Main.Domain.Settings
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
        /// Gets or Sets <see cref="JwtAuthority"/>
        /// </summary>
        public string JwtAuthority { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtAudience"/>
        /// </summary>
        public string JwtAudience { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtAudiences"/>
        /// </summary>
        public IList<string> JwtAudiences => [.. JwtAudience.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim())];

        /// <summary>
        /// Gets or Sets <see cref="JwtExpireMinutes"/>
        /// </summary>
        public double JwtExpireMinutes { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="JwtExpireDays"/>
        /// </summary>
        public double JwtExpireDays { get; set; }
    }
}
