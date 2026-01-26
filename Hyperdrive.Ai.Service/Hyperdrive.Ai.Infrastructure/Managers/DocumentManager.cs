using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Exceptions;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using Hyperdrive.Ai.Infrastructure.Contexts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="DocumentManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDocumentManager"/>
/// </summary>
public class DocumentManager(IApplicationContext context,
                             ILogger<DocumentManager> logger) : BaseManager(context), IDocumentManager
{
    /// <summary>
    /// Adds Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public async Task<Entities.Document> AddDocument(Entities.Document entity)
    {
        await CheckFileId(@entity.FileId);

        Entities.Document @document = new()
        {
            CreatedBy = entity.CreatedBy,
            FileId = entity.FileId,
            FileName = entity.FileName,
        };

        try
        {
            await Context.Document.AddAsync(@document);

            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await CheckFileId(@entity.FileId);
        }

        // Log
        string @logData = $"{nameof(Entities.Document)} {document.Id} was added at {DateTime.UtcNow:t}";

        logger.LogInformation(logData);

        return @document;
    }

    /// <summary>
    /// Checks File Id
    /// </summary>
    /// <param name="fileid">Injected <see cref="Guid"/></param>       
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> CheckFileId(Guid @fileid)
    {
        var @found = await Context.Document
                    .AsNoTracking()
                    .AsSplitQuery()
                    .TagWith("CheckFileId")
                    .Where(x => x.FileId == fileid)
                    .AnyAsync();

        if (@found)
        {
            string @logData = $"{nameof(Entities.Document)} with FileId {@fileid} was already found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Entities.Document)
                                       + " with FileId "
                                       + @fileid
                                       + " already exists");
        }

        return @found;
    }

    /// <summary>
    /// Checks File Id
    /// </summary>
    /// <param name="fileid">Injected <see cref="Guid"/></param>       
    /// <param name="id">Injected <see cref="Guid"/></param>    
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> CheckFileId(Guid @fileid, Guid id)
    {
        var @found = await Context.Document
                    .TagWith("CheckFileId")
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Where(x => x.FileId == @fileid && x.Id != @id)
                    .AnyAsync();

        if (@found)
        {
            string @logData = $"{nameof(Entities.Document)} with FileId {@fileid} was already found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Entities.Document)
                                       + " with FileId "
                                       + @fileid
                                       + " already exists");
        }

        return @found;
    }

    /// <summary>
    /// Finds Document By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public async Task<Entities.Document> FindDocumentById(Guid @id)
    {
        Entities.Document @document = await Context.Document
        .TagWith("FindDocumentById")
        .Where(x => x.Id == @id)
        .FirstOrDefaultAsync();

        if (@document == null)
        {
            string @logData = $"{nameof(Entities.Document)} with Id {@id} was not found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Entities.Document)
                                       + " with Id "
                                       + @id
                                       + " does not exist");
        }

        return @document;
    }

    /// <summary>
    /// Removes Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Document"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveDocument(Entities.Document @entity)
    {
        try
        {
            Entities.Document @document = await FindDocumentById(@entity.Id);

            @document.DeletedBy = @entity.DeletedBy;
            @document.DeletedAt = DateTime.UtcNow;
            @document.Deleted = true;

            Context.Document.Update(@document);

            await Context.SaveChangesAsync();

            string @logData = $"{nameof(Entities.Document)} with Id {@document.Id} was removed at {DateTime.UtcNow:t}";

            logger.LogInformation(@logData);
        }
        catch (DbUpdateConcurrencyException)
        {
            await FindDocumentById(@entity.Id);
        }
    }

    /// <summary>
    /// Updates Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Entities.Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public async Task<Entities.Document> UpdateDocument(Entities.Document @entity)
    {
        await CheckFileId(@entity.FileId, @entity.Id);

        Entities.Document @document = await FindDocumentById(@entity.Id);
        @document.ModifiedBy = @entity.ModifiedBy;
        @document.ModifiedAt = DateTime.UtcNow;
        @document.FileName = @entity.FileName;

        try
        {
            Context.Document.Update(@document);

            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await CheckFileId(@entity.FileId, @entity.Id);
        }

        string @logData = $"{nameof(Entities.Document)} with Id {@document.Id} was modified at {DateTime.UtcNow:t}";

        logger.LogInformation(@logData);

        return @document;
    }

    /// <summary>
    /// Reloads Document By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DocumentDto}"/></returns>
    public async Task<DocumentDto> ReloadDocumentById(Guid @id)
    {
        DocumentDto @document = await Context.Document
        .TagWith("ReloadDocumentById")
        .Where(x => x.Id == @id)
        .Select(x => x.ToDto())
        .FirstOrDefaultAsync();

        if (@document is null)
        {

            string @logData = $"{nameof(Entities.Document)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Entities.Document)
                                       + " does not exist");
        }

        return @document;
    }
}
