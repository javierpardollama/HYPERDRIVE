using System.Collections.Generic;

using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateArchive"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateArchive : UpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateArchive"/>
        /// </summary>
        public UpdateArchive()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="By"/>
        /// </summary>
        public virtual ViewApplicationUser By { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Folder"/>
        /// </summary>
        public bool Folder { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Locked"/>
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Data"/>
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Size"/>
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Type"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUsersId"/>
        /// </summary>
        public virtual ICollection<int> ApplicationUsersId { get; set; }
    }
}
