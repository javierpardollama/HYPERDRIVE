using System.Collections.Generic;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateDriveItem"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateDriveItem : UpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateDriveItem"/>
        /// </summary>
        public UpdateDriveItem()
        {
        }

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
        public string Data { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Size"/>
        /// </summary>
        public float? Size { get; set; }

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
