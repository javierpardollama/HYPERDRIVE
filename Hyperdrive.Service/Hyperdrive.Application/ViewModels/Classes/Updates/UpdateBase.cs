using Hyperdrive.Tier.ViewModels.Interfaces.Updates;

using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateBase"/> class. Inplemennts <see cref="IUpdateBase"/>
    /// </summary>
    public class UpdateBase : IUpdateBase
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
