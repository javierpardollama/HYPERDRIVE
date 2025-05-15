using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Auth
{
    /// <summary>
    /// Represents a <see cref="AuthJoinIn"/> class.
    /// </summary>
    public class AuthJoinIn
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="AuthJoinIn"/>
        /// </summary>
        public AuthJoinIn()
        {
        }

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
