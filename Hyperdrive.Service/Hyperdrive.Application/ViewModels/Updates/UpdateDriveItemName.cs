using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateDriveItemName"/> class. Inherits <see cref="UpdateBase"/>
    /// </summary>
    public class UpdateDriveItemName : UpdateBase
    {
        /// <summary>
        /// Gets or Sets <see cref="ParentId"/>
        /// </summary>      
        public int? ParentId { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="Extension"/>
        /// </summary>
        public string Extension { get; set; }
    }
}
