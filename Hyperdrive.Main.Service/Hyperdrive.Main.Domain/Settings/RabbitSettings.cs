namespace Hyperdrive.Main.Domain.Settings;

/// <summary>
/// Represents a <see cref="RabbitSettings"/> class
/// </summary>
public class RabbitSettings
{
    /// <summary>
    /// Gets or Sets <see cref="Url"/>
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Queue"/>
    /// </summary>
    public string Queue { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Key"/>
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Mandatory"/>
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ClientProvider"/>
    /// </summary>
    public string ClientProvider { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="AutomaticRecoveryEnabled"/>
    /// </summary>
    public bool AutomaticRecoveryEnabled { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="TopologyRecoveryEnabled"/>
    /// </summary>
    public bool TopologyRecoveryEnabled { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="TopologyRecoveryEnabled"/>
    /// </summary>
    public bool PublisherConfirmationsEnabled { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="TopologyRecoveryEnabled"/>
    /// </summary>
    public bool PublisherConfirmationTrackingEnabled { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Durable"/>
    /// </summary>
    public bool Durable { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="exclusive"/>
    /// </summary>
    public bool Exclusive { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="AutoDelete"/>
    /// </summary>
    public bool AutoDelete { get; set; }
}
