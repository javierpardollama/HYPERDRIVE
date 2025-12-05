using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityNameChange"/> class. Inherits <see cref="SecurityBase"/>
    /// </summary>
    public class SecurityNameChange : SecurityBase
    {
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
