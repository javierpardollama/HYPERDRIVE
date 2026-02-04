using Hyperdrive.Main.Application.ViewModels.Additions;
using Hyperdrive.Main.Application.ViewModels.Filters;
using Hyperdrive.Main.Application.ViewModels.Updates;
using Hyperdrive.Main.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Test.Service.Controllers;

[TestFixture]
public class DriveItemControllerTest : BaseControllerTest
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:55897/api/v1/driveitem/") };

    private ViewDriveItem Archive { get; set; }

    [SetUp]
    public new void SetUp()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
    }

    [Test, Order(1)]
    public async Task FindPaginatedDriveItemByApplicationUserId()
    {
        var content = JsonContent.Create(new FilterPageDriveItem { Index = 0, Size = 20, ApplicationUserId = User.Id });

        var response = await Client.PostAsync("page", content);
        response.EnsureSuccessStatusCode();
        var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItem>>();

        Assert.Pass();
    }

    [Test, Order(2)]
    public async Task FindPaginatedSharedDriveItemByApplicationUserId()
    {
        var content = JsonContent.Create(new FilterPageDriveItem { Index = 0, Size = 20, ApplicationUserId = User.Id });

        var response = await Client.PostAsync("page/shared", content);
        response.EnsureSuccessStatusCode();
        var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItem>>();

        Assert.Pass();
    }

    [Test, Order(3)]
    public async Task AddParentDriveItem()
    {
        var content = JsonContent.Create(new AddDriveItem
        {
            Data = null,
            FileName = "Root",
            Folder = true,
            ApplicationUserId = User.Id
        });

        var response = await Client.PostAsync("up", content);
        response.EnsureSuccessStatusCode();
        Archive = await response.Content.ReadFromJsonAsync<ViewDriveItem>();

        Assert.Pass();
    }

    [Test, Order(4)]
    public async Task UpdateDriveItemName()
    {
        var content = JsonContent.Create(new UpdateDriveItemName
        {
            Extension = Archive.Extension,
            Name = "Source",
            ParentId = Archive.Parent?.Id,
            Id = Archive.Id,
            ApplicationUserId = User.Id
        });

        var response = await Client.PostAsync("name/change", content);
        response.EnsureSuccessStatusCode();
        Archive = await response.Content.ReadFromJsonAsync<ViewDriveItem>();

        Assert.Pass();
    }


    [Test, Order(5)]
    public async Task RemoveDriveItemById()
    {
        var response = await Client.DeleteAsync($"remove/{Archive.Id}");
        response.EnsureSuccessStatusCode();

        Assert.Pass();
    }
}
