using Hyperdrive.Domain.Entities;
using Hyperdrive.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hyperdrive.Test.Infrastructure.Extensions
{
    /// <summary>
    /// Represents a <see cref="ContextExtension"/> class.
    /// </summary>
    public static class ContextExtension
    {
        /// <summary>
        /// Seeds
        /// </summary>
        /// <param name="this">INjected <see cref="ApplicationContext"/></param>
        public static void Seed(this ApplicationContext @this)
        {
            @this.Database.EnsureDeleted();
            @this.Database.EnsureCreated();

            if (!@this.Roles.Any())
            {
                @this.Roles.Add(new ApplicationRole
                {
                    Id = 1,
                    Name = "Dungeon Master",
                    NormalizedName = "DUNGEON_MASTER",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Dungeon_Master_500px.png"
                });
                @this.Roles.Add(new ApplicationRole
                {
                    Id = 2,
                    Name = "Paladin",
                    NormalizedName = "PALADIN",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Paladin_500px.png"
                });
                @this.Roles.Add(new ApplicationRole
                {
                    Id = 3,
                    Name = "Sorceress",
                    NormalizedName = "SORCERESS",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Sorceress_2_500px.png"
                });
                @this.Roles.Add(new ApplicationRole
                {
                    Id = 4,
                    Name = "Rogue",
                    NormalizedName = "ROGUE",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Rogue_2_500px.png"
                });
                @this.Roles.Add(new ApplicationRole
                {
                    Id = 5,
                    Name = "Bard",
                    NormalizedName = "BARD",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    ImageUri = "URL/Bard_500px.png"
                });

                @this.SaveChanges();
            }

            if (!@this.Users.Any())
            {
                @this.Users.Add(new ApplicationUser
                {
                    Id = 1,
                    FirstName = "Stafford",
                    LastName = "Parker",
                    UserName = "stafford.parker",
                    Email = "stafford.parker@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });
                @this.Users.Add(new ApplicationUser
                {
                    Id = 2,
                    FirstName = "Dee",
                    LastName = "Sandy",
                    UserName = "dee.sandy",
                    Email = "dee.sandy@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });
                @this.Users.Add(new ApplicationUser
                {
                    Id = 3,
                    FirstName = "Orinda Navy",
                    LastName = "Navy",
                    UserName = "orinda.navy",
                    Email = "orinda.navy@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });
                @this.Users.Add(new ApplicationUser
                {
                    Id = 4,
                    FirstName = "Genesis",
                    LastName = "Gavin",
                    UserName = "genesis.gavin",
                    Email = "genesis.gavin@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });
                
                @this.Users.Add(new ApplicationUser
                {
                    Id = 5,
                    FirstName = "Antonietta ",
                    LastName = "Torcuil",
                    UserName = "antonietta.torcuil",
                    Email = "antonietta.torcuil@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
                });

                @this.Users.Add(new ApplicationUser
                {
                    Id = 6,
                    FirstName = "Alessa",
                    LastName = "Simona",
                    UserName = "alessa.simona",
                    Email = "alessa.simona@email.com",
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false,
                    SecurityStamp = new Guid().ToString()
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
                        Name = "Wanabe",
                        NormalizedName = "WANABE",
                        FileName = "Wanabe.mp3",
                        NormalizedFileName = "WANABE.MP3",
                        Extension = "mp3",
                        NormalizedExtension = "MP3",
                        Type = "audio/mpeg",
                        Size = 120,
                        Data = new byte[25],
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

            if (!@this.UserRefreshTokens.Any())
            {
                @this.UserRefreshTokens.Add(new ApplicationUserRefreshToken
                {
                    Value = "i5E%@VRMZ)%3AuWuA+A+%PAcEE0q.x",
                    Name = new Guid().ToString(),
                    LoginProvider = "https://localhost:7297",
                    ExpiresAt = DateTime.UtcNow.AddDays(2),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Deleted = false,
                    UserId = 10,
                    User = new ApplicationUser
                    {
                        Id = 10,
                        FirstName = "Cali ",
                        LastName = "Trafford",
                        UserName = "cali.trafford",
                        Email = "cali.trafford@email.com",
                        CreatedAt = DateTime.UtcNow,
                        Deleted = false,
                        SecurityStamp = new Guid().ToString()
                    }
                });
                @this.UserRefreshTokens.Add(new ApplicationUserRefreshToken
                {
                    Value = "&91eVg+82z*q5qfwCLp.*f=x)];]27",
                    Name = new Guid().ToString(),
                    LoginProvider = "https://localhost:7297",
                    Revoked = false,
                    ExpiresAt = DateTime.UtcNow.AddDays(2),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Deleted = false,
                    UserId = 11,
                    User = new ApplicationUser
                    {
                        Id = 11,
                        FirstName = "Barb Román",
                        LastName = "Román",
                        UserName = "barb.román",
                        Email = "barb.román@email.com",
                        CreatedAt = DateTime.UtcNow,
                        Deleted = false,
                        SecurityStamp = new Guid().ToString()
                    }
                });

                @this.SaveChanges();
            }           
        }
    }
}
