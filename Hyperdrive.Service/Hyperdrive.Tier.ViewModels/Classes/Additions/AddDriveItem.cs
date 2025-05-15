using Hyperdrive.Tier.ViewModels.Interfaces.Additions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Folder"/>
        /// </summary>
        [Required]
        public bool Folder { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Locked"/>
        /// </summary>
        [Required]
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
