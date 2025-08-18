using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityNameChange"/> class.
    /// </summary>
    public class SecurityNameChange
    {
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
