namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    /// <summary>
    /// Represents a <see cref="SecurityPasswordReset"/> class.
    /// </summary>
    public class SecurityPasswordReset
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityPasswordReset"/>
        /// </summary>
        public SecurityPasswordReset()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="NewPassword"/>
        /// </summary>
        public string NewPassword { get; set; }
    }
}
