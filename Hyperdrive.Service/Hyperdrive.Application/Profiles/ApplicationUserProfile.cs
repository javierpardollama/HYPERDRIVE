using System.Linq;
using Hyperdrive.Application.ViewModels.Views;
using Hyperdrive.Domain.Dtos;

namespace Hyperdrive.Application.Profiles;

public static class ApplicationUserProfile
{
    public static ViewApplicationUser ToViewModel(this ApplicationUserDto @dto)
    {
        return new ViewApplicationUser
        {
            Id = @dto.Id,
            LastModified = @dto.LastModified,
            ApplicationRoles = @dto.ApplicationRoles
                .Select(x=>x.ToCatalogViewModel())
                .ToList(),
            Token = @dto.Token.ToViewModel(),
            RefreshToken = @dto.RefreshToken.ToViewModel(),
            FirstName = @dto.FirstName,
            LastName = @dto.LastName,
            Email = @dto.Email,
            PhoneNumber = @dto.PhoneNumber,
            Initial = @dto.Initial
        };
    }
    
   
}