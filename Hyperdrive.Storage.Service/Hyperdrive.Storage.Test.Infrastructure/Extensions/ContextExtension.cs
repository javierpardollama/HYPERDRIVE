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

        if (!@this.Users.Any())
        {
            @this.Users.Add(new ApplicationUser
            {
                Id = 1,
                FirstName = "Stafford",
                LastName = "Parker",
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
               
            });
            @this.Users.Add(new ApplicationUser
            {
                Id = 2,
                FirstName = "Dee",
                LastName = "Sandy",
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            });
            @this.Users.Add(new ApplicationUser
            {
                Id = 3,
                FirstName = "Orinda Navy",
                LastName = "Navy",
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            });
            @this.Users.Add(new ApplicationUser
            {
                Id = 4,
                FirstName = "Genesis",
                LastName = "Gavin",
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            });

            @this.Users.Add(new ApplicationUser
            {
                Id = 5,
                FirstName = "Antonietta ",
                LastName = "Torcuil",
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            });

            @this.Users.Add(new ApplicationUser
            {
                Id = 6,
                FirstName = "Alessa",
                LastName = "Simona",
                CreatedAt = DateTime.UtcNow,
                Deleted = false
            });

            @this.SaveChanges();
        }

        if (!@this.DriveItems.Any())
        {
            @this.DriveItems.Add(new DriveItem
            {
                Id = 2,
                Folder = true,
                By = @this.Users.First(x => x.Id == 1),
                ById = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new() {
                    Id = 21,
                    DriveItemId = 2,
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
                Id = 3,
                Folder = true,
                By = @this.Users.First(x => x.Id == 1),
                ById = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new()
                {
                    Id = 31,
                    DriveItemId = 3,
                    Name = "Music",
                    NormalizedName = "MUSIC",
                    FileName = "Music",
                    NormalizedFileName = "MUSIC",
                    CreatedAt = DateTime.UtcNow,
                }]
            });
            @this.DriveItems.Add(new DriveItem
            {
                Id = 4,
                Folder = true,
                By = @this.Users.First(x => x.Id == 1),
                ById = 1,
                Parent = null,
                ParentId = null,
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
                Activity = [
                    new()
                {
                    Id = 41,
                    DriveItemId = 4,
                    Name = "Pictures",
                    NormalizedName = "PICTURES",
                    FileName = "Pictures",
                    NormalizedFileName = "PICTURES",
                    CreatedAt = DateTime.UtcNow,
                }]
            });
            @this.DriveItems.Add(new DriveItem
            {
                Id = 5,
                Folder = true,
                By = @this.Users.First(x => x.Id == 1),
                Parent = new DriveItem
                {
                    Id = 1,
                    Folder = true,
                    By = @this.Users.First(x => x.Id == 1),
                    ById = 1,
                    Parent = null,
                    ParentId = null,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    Activity = [
                    new()
                {
                    Id = 11,
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
                    Id = 51,
                    DriveItemId = 5,
                    Name = "Wanabe",
                    NormalizedName = "WANABE",
                    FileName = "Wanabe.mp3",
                    NormalizedFileName = "WANABE.MP3",
                    Extension = "mp3",
                    NormalizedExtension = "MP3",
                    Content = new(){
                        Id = 501,
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
                DriveItem = @this.DriveItems.First(x => x.Id == 5),
                User = @this.Users.First(x => x.Id == 3),
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
            });

            @this.ApplicationUserDriveItems.Add(new ApplicationUserDriveItem
            {
                DriveItem = @this.DriveItems.First(x => x.Id == 5),
                User = @this.Users.First(x => x.Id == 3),
                CreatedAt = DateTime.UtcNow,
                Deleted = false,
            });

            @this.SaveChanges();
        }
    }
}
