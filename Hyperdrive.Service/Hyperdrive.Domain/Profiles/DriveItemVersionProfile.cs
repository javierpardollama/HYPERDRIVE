using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Profiles;

public static class DriveItemVersionProfile
{
    public static DriveItemVersionDto ToDto(this DriveItemVersion @entity)
    {
        return new DriveItemVersionDto
        {
            Id = @entity.Id,
            LastModified = @entity.LastModified,
            Data = @entity.Data,
            Size = @entity.Size,
            Type = @entity.Type,
        };
    }
}