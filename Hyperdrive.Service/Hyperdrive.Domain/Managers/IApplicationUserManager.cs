using System.Collections.Generic;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    /// <summary>
    /// Represents a <see cref="IApplicationUserManager"/> interface. Inherits <see cref="IBaseManager"/>
    /// </summary>
    public interface IApplicationUserManager : IBaseManager
    {
        /// <summary>
        /// Finds All Application User
        /// </summary>
        /// <returns>Instance of <see cref="Task{ICollection{CatalogDto}}"/></returns>
        public Task<ICollection<CatalogDto>> FindAllApplicationUser();

        /// <summary>
        /// Finds Paginated Application User
        /// </summary>
        /// <param name="index">Injected <see cref="int"/></param>
        /// <param name="size">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{PageDto{ApplicationUserDto}}"/></returns>
        public Task<PageDto<ApplicationUserDto>> FindPaginatedApplicationUser(int index, int size);

        /// <summary>
        /// Finds Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public Task<ApplicationUser> FindApplicationUserById(int @id);
        
        /// <summary>
        /// Finds All Application User By Ids
        /// </summary>
        /// <param name="ids">Injected <see cref="ICollection{int}"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public Task<IList<ApplicationUser>> FindAllApplicationUserByIds(ICollection<int> @ids);
        
        /// <summary>
        /// Finds Application User By Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUser}"/></returns>
        public Task<ApplicationUser> FindApplicationUserByEmail(string @email);

        /// <summary>
        /// Removes Application User
        /// </summary>
        /// <param name="@user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public Task RemoveApplicationUser(ApplicationUser @user);

        /// <summary>
        /// Updates Application User
        /// </summary>
        /// <param name="roles">Injected <see cref="ICollection{string}"/></param>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        public Task<ApplicationUserDto> UpdateApplicationUserRoles(ICollection<string> @roles, ApplicationUser @user);
        
        /// <summary>
        /// Adds Application Roles to Application User
        /// </summary>
        /// <param name="roles">Injected <see cref="ICollection{string}"/></param>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> AddApplicationUserRoles(ICollection<string> @roles, ApplicationUser @user);
        
        /// <summary>
        /// Removes Application Roles from Application User
        /// </summary>
        /// <param name="user">Injected <see cref="ApplicationUser"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> RemoveApplicationUserRoles(ApplicationUser @user);
        
        /// <summary>
        /// Checks Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckEmail(string @email, int @id);
        
        /// <summary>
        /// Checks Email
        /// </summary>
        /// <param name="email">Injected <see cref="string"/></param>
        /// <returns>Instance of <see cref="Task{bool}"/></returns>
        public Task<bool> CheckEmail(string @email);
        
        /// <summary>
        /// Reloads Application User By Id
        /// </summary>
        /// <param name="id">Injected <see cref="int"/></param>
        /// <returns>Instance of <see cref="Task{ApplicationUserDto}"/></returns>
        public Task<ApplicationUserDto> ReloadApplicationUserById(int @id);
    }
}
