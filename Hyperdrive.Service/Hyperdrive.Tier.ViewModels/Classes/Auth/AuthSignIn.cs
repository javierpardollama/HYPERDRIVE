namespace Hyperdrive.Tier.ViewModels.Classes.Auth
{
    /// <summary>
    /// Represents a <see cref="AuthSignIn"/> class.
    /// </summary>
    public class AuthSignIn
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="AuthSignIn"/>
        /// </summary>
        public AuthSignIn()
        {
        }

        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Password"/>
        /// </summary>
        public string Password { get; set; }
    }
}
