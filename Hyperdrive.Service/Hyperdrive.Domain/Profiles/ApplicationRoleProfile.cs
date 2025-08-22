using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class ApplicationRoleProfile
{
    public static ApplicationRoleDto ToDto(this ApplicationRole @entity)
    {
        return new ApplicationRoleDto
        {
           Id = @entity.Id,
           Name = @entity.Name,
           ImageUri =  @entity.ImageUri,
           LastModified = @entity.LastModified
        };
    }

    public static CatalogDto ToCatalog(this ApplicationRole @entity)
    {
        return new CatalogDto
        {
            Id = @entity.Id,
            Name = @entity.Name
        };
    }
}