using Hyperdrive.Storage.Domain.Entities;
using Hyperdrive.Storage.Infrastructure.Contexts;

namespace Hyperdrive.Storage.Test.Infrastructure.Extensions;

/// <summary>
/// Represents a <see cref="ContextExtension"/> class.
/// </summary>
public static class ContextExtension
{
    /// <summary>
    /// Seeds
    /// </summary>
    /// <param name="this">Injected <see cref="ApplicationContext"/></param>
    public static void Seed(this ApplicationContext @this)
    {
        @this.Database.EnsureDeleted();
        @this.Database.EnsureCreated();
        
        if (!@this.DriveItems.Any())
        {
            @this.DriveItems.Add(new DriveItem
            {
                Id = new Guid("74d31ef5-cfc3-41aa-be76-0059c570f4bd"), //2
                Folder = true,
                ByExternalId = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new() {
                    Id = new Guid("d0f0f098-8b74-44ad-b811-b80aa405bf0a"), //21
                    DriveItemId = new Guid("74d31ef5-cfc3-41aa-be76-0059c570f4bd"), //2
                    Name = "Documents",
                    NormalizedName = "DOCUMENTS",
                    FileName = "Documents",
                    NormalizedFileName = "DOCUMENTS",
                    CreatedAt = DateTime.UtcNow,
                }
            ]
            });
            @this.DriveItems.Add(new DriveItem
            {
                Id = new Guid("6c382d05-3869-412b-bc3b-f05285da45b8"), //3
                Folder = true,
                ByExternalId = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new()
                {
                    Id = new Guid("cb7a3919-5ee7-492c-a8c4-c821e06249e1"), // 31,
                    DriveItemId = new Guid("6c382d05-3869-412b-bc3b-f05285da45b8"), //3
                    Name = "Music",
                    NormalizedName = "MUSIC",
                    FileName = "Music",
                    NormalizedFileName = "MUSIC",
                    CreatedAt = DateTime.UtcNow,
                }]
            });
            @this.DriveItems.Add(new DriveItem
            {
                Id = new Guid("3c191229-c808-46bc-90ff-2df1cf3415f5"), //4
                Folder = true,
                ByExternalId = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new()
                {
                    Id = new Guid("cc8f2ecb-c43b-48da-a749-b5c820e7ccd6"), //41
                    DriveItemId = new Guid("3c191229-c808-46bc-90ff-2df1cf3415f5"), //4
                    Name = "Pictures",
                    NormalizedName = "PICTURES",
                    FileName = "Pictures",
                    NormalizedFileName = "PICTURES",
                    CreatedAt = DateTime.UtcNow,
                }]
            });
            @this.DriveItems.Add(new DriveItem
            {
                Id = new Guid("f0354da5-dd9e-4f70-8379-7a0f7dfc67ec"), //5
                Folder = true,
                ByExternalId = 1,
                Parent = new DriveItem
                {
                    Id = new Guid("9bd5be02-f392-4ca1-923c-6c9753b645e5"), //1
                    Folder = true,
                    ByExternalId = 1,
                    Parent = null,
                    ParentId = null,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    Activity = [
                    new()
                {
                    Id = new Guid("38315ea0-63f5-4549-89e6-55b5e7cf2a04"), //11
                    Name = "Shared",
                    NormalizedName = "SHARED",
                    FileName = "Shared",
                    NormalizedFileName = "SHARED",
                    CreatedAt = DateTime.UtcNow,
                }],
                },
                Activity = [
                    new()
                {
                    Id = new Guid("f48fcf02-4c03-45f0-a884-9d7d11f6bccc"), //51
                    DriveItemId = new Guid("f0354da5-dd9e-4f70-8379-7a0f7dfc67ec"), //5
                    Name = "Wanabe",
                    NormalizedName = "WANABE",
                    FileName = "Wanabe.mp3",
                    NormalizedFileName = "WANABE.MP3",
                    Extension = "mp3",
                    NormalizedExtension = "MP3",
                    Content = new(){
                        Id = new Guid("2a6062d8-f851-4446-82e3-7dd9521b0814"), //501
                        Size = 120,
                        Type = "audio/mpeg",
                        Data = new byte[25],
                        CreatedAt = DateTime.UtcNow
                    },
                    CreatedAt = DateTime.UtcNow,
                }],
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
            });

            @this.SaveChanges();
        }

        if (!@this.ApplicationUserDriveItems.Any())
        {
            @this.ApplicationUserDriveItems.Add(new ApplicationUserDriveItem
            {
                DriveItem = @this.DriveItems.First(x => x.Id == new Guid("f0354da5-dd9e-4f70-8379-7a0f7dfc67ec")), //5
                ExternalUserId = 3,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
            });

            @this.ApplicationUserDriveItems.Add(new ApplicationUserDriveItem
            {
                DriveItem = @this.DriveItems.First(x => x.Id == new Guid("f0354da5-dd9e-4f70-8379-7a0f7dfc67ec")), //5
                ExternalUserId = 3,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
            });

            @this.SaveChanges();
        }
    }
}
