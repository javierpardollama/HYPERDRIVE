using Hyperdrive.Ai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Managers;

/// <summary>
/// Represents a <see cref="IAnswerManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IAnswerManager : IBaseManager
{
    /// <summary>
    /// Finds Latest Answers By Chat Id
    /// </summary>
    /// <param name="chatid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="ICollection{Entities.Answer}"/></returns>
    public Task<ICollection<Answer>> FindLatestAnswersByChatId(Guid @chatid);
}
