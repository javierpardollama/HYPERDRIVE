using System.Collections.Generic;

using Hyperdrive.Tier.ViewModels.Interfaces.Updates;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateApplicationUser"/> class. Implements <see cref="IUpdateBase"/>
    /// </summary>
    public class UpdateApplicationUser : IUpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateApplicationUser"/>
        /// </summary>
        public UpdateApplicationUser()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationRolesId"/>
        /// </summary>
        public virtual ICollection<int> ApplicationRolesId { get; set; }
    }
}
