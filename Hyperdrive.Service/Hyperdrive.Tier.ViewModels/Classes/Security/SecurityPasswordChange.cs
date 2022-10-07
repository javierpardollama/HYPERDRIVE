using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPasswordChange"/> class.
    /// </summary>
    public class SecurityPasswordChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityPasswordChange"/>
        /// </summary>
        public SecurityPasswordChange()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        public virtual ViewApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="CurrentPassword"/>
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPassword"/>
        /// </summary>
        public string NewPassword { get; set; }
    }
}
