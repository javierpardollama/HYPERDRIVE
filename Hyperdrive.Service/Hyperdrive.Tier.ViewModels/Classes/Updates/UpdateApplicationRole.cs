using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateApplicationRole"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateApplicationRole : UpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateApplicationRole"/>
        /// </summary>
        public UpdateApplicationRole()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ImageUri"/>
        /// </summary>
        [Required]
        [Url]
        public string ImageUri { get; set; }
    }
}
