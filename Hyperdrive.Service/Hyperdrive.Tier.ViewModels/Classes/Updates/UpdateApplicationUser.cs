﻿using System.Collections.Generic;

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
        /// Gets or Sets <see cref="ApplicationRolesId"/>
        /// </summary>
        public virtual ICollection<int> ApplicationRolesId { get; set; }
    }
}
