using Hyperdrive.Tier.ViewModels.Interfaces.Additions;

namespace Hyperdrive.Tier.ViewModels.Classes.Additions
{
    /// <summary>
    /// Represents a <see cref="AddApplicationRole"/> class. Implements <see cref="IAddBase"/>
    /// </summary>
    public class AddApplicationRole : IAddBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="AddApplicationRole"/>
        /// </summary>
        public AddApplicationRole()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ImageUri"/>
        /// </summary>
        public string ImageUri { get; set; }
    }
}
