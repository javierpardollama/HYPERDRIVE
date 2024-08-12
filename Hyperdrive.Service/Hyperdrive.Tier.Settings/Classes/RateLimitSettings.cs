namespace Hyperdrive.Tier.Settings.Classes
{
    /// <summary>
    /// Represents a <see cref="RateLimitSettings"/> class
    /// </summary>
    public class RateLimitSettings
    {
        /// <summary>
        /// Gets or Sets <see cref="PolicyName"/>
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="PermitLimit"/>
        /// </summary>
        public int PermitLimit { get; set; }       

        /// <summary>
        /// Gets or Sets <see cref="QueueProcessingOrder"/>
        /// </summary>
        public int QueueProcessingOrder { get;set;}

        /// <summary>
        /// Gets or Sets <see cref="QueueLimit"/>
        /// </summary>
        public int QueueLimit { get; set; }
    }
}
