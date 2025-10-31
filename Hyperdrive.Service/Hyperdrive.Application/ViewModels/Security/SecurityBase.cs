using Hyperdrive.Application.ViewModels.Interfaces.Security;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityBase"/> class. Implements <see cref="ISecurityBase"/>
    /// </summary>
    public class SecurityBase : ISecurityBase
    {       
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }
    }
}
