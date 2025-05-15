using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    public class SecurityNameChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityEmailChange"/>
        /// </summary>
        public SecurityNameChange()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewFirstName"/>
        /// </summary>
        [Required]
        public string NewFirstName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewLastName"/>
        /// </summary>
        [Required]
        public string NewLastName { get; set; }
    }
}
