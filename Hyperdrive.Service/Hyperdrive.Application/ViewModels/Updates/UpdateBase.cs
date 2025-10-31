using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateBase"/> class.
    /// </summary>
    public class UpdateBase
    {
        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        [Required]
        public int ApplicationUserId { get; set; }
    }
}
