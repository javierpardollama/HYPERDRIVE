using Hyperdrive.Tier.ViewModels.Interfaces.Updates;

namespace Hyperdrive.Tier.ViewModels.Classes.Updates
{
    /// <summary>
    /// Represents a <see cref="UpdateBase"/> class. Inplemennts <see cref="IUpdateBase"/>
    /// </summary>
    public class UpdateBase : IUpdateBase
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="UpdateBase"/>
        /// </summary>
        public UpdateBase()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public int ApplicationUserId { get; set; }
    }
}
