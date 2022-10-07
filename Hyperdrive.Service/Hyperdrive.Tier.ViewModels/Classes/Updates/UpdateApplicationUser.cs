using System.Collections.Generic;

using Hyperdrive.Tier.ViewModels.Interfaces.Updates;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateApplicationUser"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateApplicationUser : UpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateApplicationUser"/>
        /// </summary>
        public UpdateApplicationUser()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="SurName"/>
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationRolesId"/>
        /// </summary>
        public virtual ICollection<int> ApplicationRolesId { get; set; }
    }
}
