using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;
using System.Linq;

namespace Hyperdrive.Application.Profiles;

/// <summary>
/// Represents a <see cref="ApplicationRoleProfile"/> class.
/// </summary>
public static class ApplicationRoleProfile
{
    /// <summary>
    /// Transforms to ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="ApplicationRoleDto"/></param>
    /// <returns>Instance of <see cref="ViewApplicationRole"/></returns>
    public static ViewApplicationRole ToViewModel(this ApplicationRoleDto @dto)
    {
        return new ViewApplicationRole
        {
           Id = @dto.Id,
           Name = @dto.Name,
           ImageUri =  @dto.ImageUri,
           LastModified = @dto.LastModified
        };
    }
    
    /// <summary>
    /// Transforms to Page ViewModel
    /// </summary>
    /// <param name="dto">Injected <see cref="PageDto{ApplicationRoleDto}"/></param>
    /// <returns>Instance of <see cref="ViewPage{ViewApplicationRole}"/></returns>
    public static ViewPage<ViewApplicationRole> ToPageViewModel(this PageDto<ApplicationRoleDto> @dto)
    {
        return new ViewPage<ViewApplicationRole>
        {
            Index = @dto.Index,
            Length = @dto.Length,
            Size = @dto.Size,
            Items = [.. dto.Items.Select(x=> x.ToViewModel())]
        };
    }
}