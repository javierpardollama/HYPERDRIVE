using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

public static class ApplicationRoleProfile
{
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
}