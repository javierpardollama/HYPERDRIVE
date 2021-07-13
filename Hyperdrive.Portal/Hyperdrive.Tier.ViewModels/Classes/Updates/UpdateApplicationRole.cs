using Hyperdrive.Tier.ViewModels.Interfaces.Updates;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateApplicationRole"/> class. Inplemennts <see cref="IUpdateBase"/>
    /// </summary>
    public class UpdateApplicationRole : IUpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateApplicationRole"/>
        /// </summary>
        public UpdateApplicationRole()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        public int Id { get; set; }

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
