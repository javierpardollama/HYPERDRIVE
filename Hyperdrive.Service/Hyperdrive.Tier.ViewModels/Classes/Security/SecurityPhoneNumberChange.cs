using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPhoneNumberChange"/> class.
    /// </summary>
    public class SecurityPhoneNumberChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityPhoneNumberChange"/>
        /// </summary>
        public SecurityPhoneNumberChange()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUser"/>
        /// </summary>
        public virtual ViewApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPhoneNumber"/>
        /// </summary>
        public string NewPhoneNumber { get; set; }
    }
}
