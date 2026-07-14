using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Exceptions;
using Hyperdrive.Intelligence.Domain.Managers;
using Hyperdrive.Intelligence.Domain.Profiles;
using Hyperdrive.Intelligence.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Document = Hyperdrive.Intelligence.Domain.Entities.Document;

namespace Hyperdrive.Intelligence.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="DocumentManager" /> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDocumentManager"/>
/// </summary>
public class DocumentManager(IApplicationContext context,
                             ILogger<DocumentManager> logger) : BaseManager(context), IDocumentManager
{
    /// <summary>
    /// Adds Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public async Task<Document> AddDocument(Document entity)
    {
        await CheckFileId(@entity.FileId);

        Document @document = new()
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
        string @logData = $"{nameof(Document)} {document.Id} was added at {DateTime.UtcNow:t}";

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
            string @logData = $"{nameof(Document)} with FileId {@fileid} was already found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Document)
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
            string @logData = $"{nameof(Document)} with FileId {@fileid} was already found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Document)
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
    public async Task<Document> FindDocumentById(Guid @id)
    {
        Document @document = await Context.Document
        .TagWith("FindDocumentById")
        .Where(x => x.Id == @id)
        .FirstOrDefaultAsync();

        if (@document == null)
        {
            string @logData = $"{nameof(Document)} with Id {@id} was not found at {DateTime.UtcNow:t}";

            logger.LogError(@logData);

            throw new ServiceException(nameof(Document)
                                       + " with Id "
                                       + @id
                                       + " does not exist");
        }

        return @document;
    }

    /// <summary>
    /// Removes Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveDocument(Document @entity)
    {
        try
        {
            Document @document = await FindDocumentById(@entity.Id);

            @document.DeletedBy = @entity.DeletedBy;
            @document.DeletedAt = @entity.DeletedAt;
            @document.Deleted = @entity.Deleted;

            Context.Document.Update(@document);

            await Context.SaveChangesAsync();

            string @logData = $"{nameof(Document)} with Id {@document.Id} was removed at {DateTime.UtcNow:t}";

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
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public async Task<Document> UpdateDocument(Document @entity)
    {
        await CheckFileId(@entity.FileId, @entity.Id);

        Document @document = await FindDocumentById(@entity.Id);
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

        string @logData = $"{nameof(Document)} with Id {@document.Id} was modified at {DateTime.UtcNow:t}";

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
        .AsNoTracking()
        .AsSplitQuery()
        .TagWith("ReloadDocumentById")
        .Where(x => x.Id == @id)
        .Select(x => x.ToDto())
        .FirstOrDefaultAsync();

        if (@document is null)
        {

            string @logData = $"{nameof(Document)} with Id {@id} was not found at at {DateTime.UtcNow:t}";

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(Document)
                                       + " does not exist");
        }

        return @document;
    }
}
