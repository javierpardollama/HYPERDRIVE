using Hyperdrive.Tier.ViewModels.Interfaces.Additions;

using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Additions
{
    /// <summary>
    /// Represents a <see cref="AddApplicationRole"/> class. Implements <see cref="IAddBase"/>
    /// </summary>
    public class AddApplicationRole : IAddBase
    {
        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }

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
