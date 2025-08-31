using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPasswordChange"/> class.
    /// </summary>
    public class SecurityPasswordChange
    {
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="CurrentPassword"/>
        /// </summary>
        [Required]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPassword"/>
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserRefreshToken"/>
        /// </summary>
        [Required]
        public string ApplicationUserRefreshToken  { get; set; }
    }
}
