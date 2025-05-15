using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPasswordChange"/> class.
    /// </summary>
    public class SecurityPasswordChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityPasswordChange"/>
        /// </summary>
        public SecurityPasswordChange()
        {
        }

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
    }
}
