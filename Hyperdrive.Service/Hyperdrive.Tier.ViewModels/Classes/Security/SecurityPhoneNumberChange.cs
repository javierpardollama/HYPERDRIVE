using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPhoneNumberChange"/> class.
    /// </summary>
    public class SecurityPhoneNumberChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityPhoneNumberChange"/>
        /// </summary>
        public SecurityPhoneNumberChange()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPhoneNumber"/>
        /// </summary>
        [Required]
        [Phone]
        public string NewPhoneNumber { get; set; }
    }
}
