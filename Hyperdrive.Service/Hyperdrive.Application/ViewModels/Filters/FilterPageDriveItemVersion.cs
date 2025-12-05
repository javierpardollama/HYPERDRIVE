using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Filters
{
    public class FilterPageDriveItemVersion
    {
        /// <summary>
        /// Gets or Sets <see cref="Index"/>
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Size"/>
        /// </summary>
        [Required]
        public int Size { get; set; }       

        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
