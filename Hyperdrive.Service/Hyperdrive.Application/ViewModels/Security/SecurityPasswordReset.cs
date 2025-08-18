using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPasswordReset"/> class.
    /// </summary>
    public class SecurityPasswordReset
    {
        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPassword"/>
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
