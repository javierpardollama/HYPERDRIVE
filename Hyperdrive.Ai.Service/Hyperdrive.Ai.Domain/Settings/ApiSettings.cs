using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Settings;

/// <summary>
///     Represents a <see cref="ApiSettings" /> class
/// </summary>
public class ApiSettings
{
    /// <summary>
    ///     Gets or Sets <see cref="ApiLock" />
    /// </summary>
    public string ApiLock { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiKey" />
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiAudiences" />
    /// </summary>
    public IList<string> ApiAudiences { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiIssuer" />
    /// </summary>
    public string ApiIssuer { get; set; }
}