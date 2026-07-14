using Hyperdrive.Intelligence.Domain.Dtos;
using Hyperdrive.Intelligence.Domain.Entities;

namespace Hyperdrive.Intelligence.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDocumentManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDocumentManager : IBaseManager
{
    /// <summary>
    /// Removes Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task RemoveDocument(Document @entity);

    /// <summary>
    /// Updates Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public Task<Document> UpdateDocument(Document @entity);

    /// <summary>
    /// Adds Document
    /// </summary>
    /// <param name="entity">Injected <see cref="Document"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public Task<Document> AddDocument(Document @entity);

    /// <summary>
    /// Finds Document By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{Entities.Document}"/></returns>
    public Task<Document> FindDocumentById(Guid @id);

    /// <summary>
    /// Checks File Id
    /// </summary>
    /// <param name="fileid">Injected <see cref="Guid"/></param>       
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public Task<bool> CheckFileId(Guid @fileid);

    /// <summary>
    /// Checks File Id
    /// </summary>
    /// <param name="fileid">Injected <see cref="Guid"/></param>       
    /// <param name="id">Injected <see cref="Guid"/></param>    
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public Task<bool> CheckFileId(Guid @fileid, Guid @id);

    /// <summary>
    /// Reloads Document By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DocumentDto}"/></returns>
    public Task<DocumentDto> ReloadDocumentById(Guid @id);
}
