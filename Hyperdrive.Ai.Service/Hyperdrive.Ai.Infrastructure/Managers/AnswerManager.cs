using Hyperdrive.Ai.Domain.Entities;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="AnswerManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IAnswerManager"/>
/// </summary>
public class AnswerManager(IApplicationContext context,
                         ILogger<AnswerManager> logger) : BaseManager(context), IAnswerManager
{
    /// <summary>
    /// Finds Latest Answers By Chat Id
    /// </summary>
    /// <param name="chatid">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="ICollection{Entities.Interaction}"/></returns>
    public async Task<ICollection<Answer>> FindLatestAnswersByChatId(Guid @chatid)
    {
        ICollection<Answer> @answers = await Context.Interactions
            .TagWith("FindLatestAnswersByChatId")
            .AsNoTracking()
            .Where(x => x.ChatId == chatid)
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .Select(x => x.Answer)
            .ToListAsync();


        return @answers;
    }
}
