using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Settings;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.Options;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="BaseManager" /> class. Implements <see cref="IBaseManager" />
/// </summary>
public class BaseManager : IBaseManager
{
    /// <summary>
    ///     Instance of <see cref="IApplicationContext" />
    /// </summary>
    protected readonly IApplicationContext Context;

    /// <summary>
    ///     Instance of <see cref="IOptions{ApiSettings}" />
    /// </summary>
    protected readonly IOptions<ApiSettings> ApiSettings;

    /// <summary>
    ///     Initializes a new Instance of <see cref="BaseManager" />
    /// </summary>
    /// <param name="context">Injected <see cref="IApplicationContext" /></param>
    protected BaseManager(IApplicationContext context)
    {
        Context = context;
    }

    /// <summary>
    ///     Initializes a new Instance of <see cref="BaseManager" />
    /// </summary>
    /// <param name="apiSettings">Injected <see cref="IOptions{ApiSettings}" /></param>
    protected BaseManager(IOptions<ApiSettings> apiSettings)
    {
        ApiSettings = apiSettings;
    }
}