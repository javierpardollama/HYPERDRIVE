using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Settings;

/// <summary>
///     Represents a <see cref="ApiSettings" /> class
/// </summary>
public class ApiSettings
{
    /// <summary>
    ///     Gets or Sets <see cref="ApiUser" />
    /// </summary>
    public string ApiUser { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiPassword" />
    /// </summary>
    public string ApiPassword { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiAudiences" />
    /// </summary>
    public IList<string> ApiAudiences { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="ApiIssuer" />
    /// </summary>
    public string ApiIssuer { get; set; }
}