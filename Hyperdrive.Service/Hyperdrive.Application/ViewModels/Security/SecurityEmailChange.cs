using Hyperdrive.Application.ViewModels.Updates;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityEmailChange"/> class. Inherits <see cref="SecurityBase"/>
    /// </summary>
    public class SecurityEmailChange : SecurityBase
    {      
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
