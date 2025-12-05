using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationRoleProfile"/> class.
/// </summary>
public static class ApplicationUserProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="ApplicationUserDto"/></param>
    /// <returns>Instance of <see cref="ViewApplicationUser"/></returns>
    public static ViewApplicationUser ToViewModel(this ApplicationUserDto @dto)
    {
        return new ViewApplicationUser
        {
            Id = @dto.Id,
            LastModified = @dto.LastModified,
            ApplicationRoles = [.. @dto.ApplicationRoles.Select(x=>x.ToCatalogViewModel())],
            Token = @dto.Token?.ToViewModel(),
            RefreshToken = @dto.RefreshToken?.ToViewModel(),
            FirstName = @dto.FirstName,
            LastName = @dto.LastName,
            Email = @dto.Email,
            PhoneNumber = @dto.PhoneNumber,
            Initial = @dto.Initial
        };
    }
    
    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{ApplicationUserDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewApplicationUser}"/></returns>
    public static ViewPage<ViewApplicationUser> ToPageViewModel(this PageDto<ApplicationUserDto> @dto)
    {
        return new ViewPage<ViewApplicationUser>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x=> x.ToViewModel())]
        };
    }
    
   
}