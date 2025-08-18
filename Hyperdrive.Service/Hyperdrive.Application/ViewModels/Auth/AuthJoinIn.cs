using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Auth
{
    /// <summary>
    /// Represents a <see cref="AuthJoinIn"/> class.
    /// </summary>
    public class AuthJoinIn
    {
        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Password"/>
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
