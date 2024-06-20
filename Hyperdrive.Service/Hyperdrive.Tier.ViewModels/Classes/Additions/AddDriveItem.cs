using System.Collections.Generic;

using Hyperdrive.Tier.ViewModels.Classes.Views;
using Hyperdrive.Tier.ViewModels.Interfaces.Additions;

namespace Hyperdrive.Tier.ViewModels.Classes.Additions
{
    /// <summary>
    /// Represents a <see cref="AddDriveItem"/> class.
    /// </summary>
    public class AddDriveItem : IAddBase
    {
        /// <summary>
        /// Represents a <see cref="AddDriveItem"/> class.
        /// </summary>
        public AddDriveItem()
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
