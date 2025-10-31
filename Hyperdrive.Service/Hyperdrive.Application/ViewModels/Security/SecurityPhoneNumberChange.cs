using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPhoneNumberChange"/> class. Inherits <see cref="SecurityBase"/>
    /// </summary>
    public class SecurityPhoneNumberChange : SecurityBase
    {      
        /// <summary>
        /// Gets or Sets <see cref="NewPhoneNumber"/>
        /// </summary>
        [Required]
        [Phone]
        public string NewPhoneNumber { get; set; }       
    }
}
