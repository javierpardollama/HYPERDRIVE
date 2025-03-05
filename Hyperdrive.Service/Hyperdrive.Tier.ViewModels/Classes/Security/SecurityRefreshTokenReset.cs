namespace Hyperdrive.Tier.ViewModels.Classes.Security
{
    public class SecurityRefreshTokenReset
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="SecurityRefreshTokenReset"/>
        /// </summary>
        public SecurityRefreshTokenReset() { }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public int ApplicationUserId { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationUserId"/>
        /// </summary>
        public string ApplicationUserRefreshToken { get; set; }
    }
}
