using Hyperdrive.Tier.ViewModels.Interfaces.Filters;

namespace Hyperdrive.Tier.ViewModels.Classes.Filters
{
    /// <summary>
    /// Represents a <see cref="FilterPageArchive"/> class. Implements <see cref="IFilterPage"/>
    /// </summary>
    public class FilterPageArchive : IFilterPage
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="FilterPageArchive"/>
        /// </summary>
        public FilterPageArchive() 
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Index"/>
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Size"/>
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public int ApplicationUserId { get; set; }
    }
}
