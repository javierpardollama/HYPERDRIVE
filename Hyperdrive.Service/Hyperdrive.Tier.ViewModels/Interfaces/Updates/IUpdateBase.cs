namespace Hyperdrive.Tier.ViewModels.Interfaces.Updates
{
    /// <summary>
    /// Represents a <see cref="IUpdateBase"/> interface
    /// </summary>
    public interface IUpdateBase
    {
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
