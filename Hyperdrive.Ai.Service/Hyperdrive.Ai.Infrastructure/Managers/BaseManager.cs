using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;

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
    ///     Initializes a new Instance of <see cref="BaseManager" />
    /// </summary>
    /// <param name="context">Injected <see cref="IApplicationContext" /></param>
    protected BaseManager(IApplicationContext context)
    {
        Context = context;
    }
}