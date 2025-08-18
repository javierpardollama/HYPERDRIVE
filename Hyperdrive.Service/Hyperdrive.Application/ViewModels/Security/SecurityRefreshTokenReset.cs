using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityRefreshTokenReset"/> class.
    /// </summary>
    public class SecurityRefreshTokenReset
    {
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public string ApplicationUserRefreshToken { get; set; }
    }
}
