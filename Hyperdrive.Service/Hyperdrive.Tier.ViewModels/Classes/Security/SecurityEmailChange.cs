namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityEmailChange"/> class.
    /// </summary>
    public class SecurityEmailChange
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityEmailChange"/>
        /// </summary>
        public SecurityEmailChange()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewEmail"/>
        /// </summary>
        public string NewEmail { get; set; }
    }
}
