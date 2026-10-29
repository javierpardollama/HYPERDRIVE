using System.Linq.Expressions;
using Hyperdrive.Storage.Domain.Dtos;
using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Domain.Exceptions;
using Hyperdrive.Storage.Domain.Managers;
using Hyperdrive.Storage.Domain.Profiles;
using Hyperdrive.Storage.Infrastructure.Contexts.Interfaces;
using Hyperdrive.Storage.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Storage.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{DriveItemManager}"/></param>
public class DriveItemManager(
    IApplicationContext context,
    ILogger<DriveItemManager> logger) : BaseManager(context), IDriveItemManager
{
    /// <summary>
    /// Finds Drive Item By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
    public async Task<DriveItem> FindDriveItemById(Guid? @id)
    {
        DriveItem @archive = await Context.DriveItems
            .TagWith("FindDriveItemById")
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (@archive is null)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " does not exist");
        }

        return @archive;
    }

    /// <summary>
    /// Finds Drive Item By FileName
    /// </summary>
    /// <param name="filename">Injected <see cref="string"/></param>
    /// <param name="parentid">Injected <see cref="Guid?"/></param>       
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
    public async Task<DriveItem> FindDriveItemByFileName(string @filename, Guid? parentid, int @userexternalid)
    {
        var @expr1 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ByExternalId == @userexternalid);
        var @expr2 = (Expression<Func<DriveItemInfo, bool>>)(x => x.FileName == @filename.Trim());
        var @expr3 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ParentId == @parentid);
        var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3);

        var @archive = await Context.DriveItemInfos
            .TagWith("FindDriveItemByFileName")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.DriveItem)
            .Where(@comboexp)
            .Select(x => x.DriveItem)
            .FirstOrDefaultAsync();

        if (@archive is null)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was already found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " already exists");
        }

        return @archive;
    }

    /// <summary>
    /// Removes Drive Item
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItem"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task RemoveDriveItem(DriveItem @entity)
    {
        Context.DriveItems.Remove(@entity);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItem)
                          + " was removed at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }

    /// <summary>
    /// Finds Paginated Drive Item By Application User Id
    /// </summary>
    /// <param name="index">Injected <see cref="int"/></param>
    /// <param name="size">Injected <see cref="int"/></param>
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <param name="parentid">Injected <see cref="Guid?"/></param>
    /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
    public async Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @userexternalid, Guid? parentid)
    {
        var @expr1 = (Expression<Func<DriveItem, bool>>)(x => x.ByExternalId == @userexternalid);
        var @expr2 = (Expression<Func<DriveItem, bool>>)(x => x.ParentId == @parentid);
        var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2);

        PageDto<DriveItemDto> @page = new()
        {
            Length = await Context.DriveItems.TagWith("CountAllDriveItemByApplicationUserId")
                .AsSplitQuery()
                .AsNoTracking()
                .Where(@comboexp)
                .CountAsync(),
            Index = @index,
            Size = @size,
            Items = (await Context.DriveItems
                .TagWith("FindPaginatedDriveItemByApplicationUserId")
                .AsSplitQuery()
                .AsNoTracking()
                .Include(x => x.Activity)
                .ThenInclude(x => x.Content)
                .Include(x => x.Parent)
                .Include(x => x.SharedWith)
                .Where(@comboexp)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(@index * @size)
                .Take(@size)
                .Select(x => x.ToDto())
                .ToListAsync())
        };

        return @page;
    }

    /// <summary>
    /// Finds Paginated Shared Drive Item By Application User Id
    /// </summary>
    /// <param name="index">Injected <see cref="int"/></param>
    /// <param name="size">Injected <see cref="int"/></param>
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{PageDto{DriveItemDto}}"/></returns>
    public async Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemWithApplicationUserId(int @index,
                                                                                               int @size,
                                                                                               int @userexternalid)
    {
        PageDto<DriveItemDto> @page = new()
        {
            Length = await Context.ApplicationUserDriveItems.TagWith("CountAllSharedDriveItemByApplicationUserId")
                .AsSplitQuery()
                .AsNoTracking()
                .Where(x => x.ExternalUserId == @userexternalid)
                .CountAsync(),
            Index = @index,
            Size = @size,
            Items = await Context.ApplicationUserDriveItems
                .TagWith("FindPaginatedSharedDriveItemByApplicationUserId")
                .AsSplitQuery()
                .AsNoTracking()
                .Include(x => x.DriveItem.Activity)
                .ThenInclude(x => x.Content)
                .Include(x => x.DriveItem.Parent)
                .Where(x => x.ExternalUserId == @userexternalid)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(@index * @size)
                .Take(@size)
                .Select(x => x.DriveItem.ToDto())
                .ToListAsync()
        };

        return @page;
    }



    /// <summary>
    /// Adds Drive Item
    /// </summary>
    /// <param name="parentid">Injected <see cref="Guid?"/></param>
    /// <param name="folder">Injected <see cref="bool"/></param>
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
    public async Task<DriveItem> AddDriveItem(Guid? parentid, bool @folder, int @userexternalid)
    {
        var @entity = new DriveItem()
        {
            Folder = @folder,
            ParentId = @parentid,
            ByExternalId = @userexternalid
        };

        await Context.DriveItems.AddAsync(@entity);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItem)
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @entity;
    }

    /// <summary>
    /// Adds Shared With
    /// </summary>
    /// <param name="users">Injected <see cref="IList{ApplicationUser}"/></param>
    /// <param name="entity">Injected <see cref="DriveItem"/></param>
    public async Task AddSharedWith(IList<int> @users, DriveItem @entity)
    {
        var @userDriveItems = users.Select(@user => new ApplicationUserDriveItem()
        {
            DriveItemId = @entity.Id,
            ExternalUserId = @user
        }).ToList();

        await Context.ApplicationUserDriveItems.AddRangeAsync(@userDriveItems);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(ApplicationUserDriveItem)
                          + " were added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }

    /// <summary>
    /// Checks File Name
    /// </summary>
    /// <param name="filename">Injected <see cref="string"/></param>
    /// <param name="parentid">Injected <see cref="Guid"/></param>
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{bool}"/></returns>
    public async Task<bool> CheckFileName(string @filename, Guid? @parentid, int @userexternalid)
    {
        var @expr1 = (Expression<Func<DriveItemInfo, bool>>)(x => x.FileName == @filename.Trim());
        var @expr2 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ParentId == @parentid);
        var @expr3 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ByExternalId == @userexternalid);
        var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3);

        var @found = await Context.DriveItemInfos
            .TagWith("CheckFileName")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.DriveItem)
            .Where(@comboexp)
            .Select(x => x.DriveItem)
            .AnyAsync();

        if (@found)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was already found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " already exists");
        }

        return @found;
    }

    /// <summary>
    /// Checks Name
    /// </summary>
    /// <param name="name">Injected <see cref="string"/></param>
    /// <param name="extension">Injected <see cref="string"/></param>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <param name="parentid">Injected <see cref="Guid?"/></param>
    /// <param name="userexternalid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItem}"/></returns>
    public async Task<bool> CheckName(string @name, Guid @id, Guid? @parentid, int userexternalid, string @extension = null)
    {
        var @expr1 = (Expression<Func<DriveItemInfo, bool>>)(x => x.Name == @name.Trim());
        var @expr2 = (Expression<Func<DriveItemInfo, bool>>)(x => @extension == null || x.Extension == @extension.Trim());
        var @expr3 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ParentId == @parentid);
        var @expr4 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItemId != @id);
        var @expr5 = (Expression<Func<DriveItemInfo, bool>>)(x => x.DriveItem.ByExternalId == @userexternalid);
        var @comboexp = ExpressionCombiner.CombineWithAnd(@expr1, @expr2, @expr3, @expr4, @expr5);

        var @found = await Context.DriveItemInfos
            .TagWith("CheckName")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.DriveItem)
            .Where(@comboexp)
            .AnyAsync();

        if (@found)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was already found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " already exists");
        }

        return @found;
    }

    /// <summary>
    /// Reloads Drive Item By Id
    /// </summary>
    /// <param name="id">Injected <see cref="Guid"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemDto}"/></returns>
    public async Task<DriveItemDto> ReloadDriveItemById(Guid @id)
    {
        DriveItemDto @archive = await Context.DriveItems
            .TagWith("ReloadDriveItemById")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Activity)
            .ThenInclude(x => x.Content)
            .Include(x => x.Parent)
            .Include(x => x.SharedWith)
            .Where(x => x.Id == @id)
            .Select(x => x.ToDto())
            .FirstOrDefaultAsync();

        if (@archive is null)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " with Id "
                                       + id
                                       + " does not exist");
        }

        return @archive;
    }
}