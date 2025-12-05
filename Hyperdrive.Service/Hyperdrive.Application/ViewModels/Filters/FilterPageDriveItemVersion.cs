using Hyperdrive.Application.ViewModels.Interfaces.Filters;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Filters
{
    /// <summary>
    /// Represents a <see cref="FilterPageDriveItemVersion"/> class. Implements <see cref="IFilterPage"/>
    /// </summary>
    public class FilterPageDriveItemVersion : IFilterPage
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
