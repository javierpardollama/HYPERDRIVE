using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityEmailChange"/> class.
    /// </summary>
    public class SecurityEmailChange
    {
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewEmail"/>
        /// </summary>
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserRefreshToken"/>
        /// </summary>
        [Required]
        public string ApplicationUserRefreshToken  { get; set; }
    }
}
