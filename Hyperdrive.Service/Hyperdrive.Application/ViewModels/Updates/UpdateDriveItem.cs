using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateDriveItem"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateDriveItem : UpdateBase
    {
        /// <summary>
        /// Gets or Sets <see cref="ParentId"/>
        /// </summary>
        [Required]
        public int ParentId { get; set; }
        
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
