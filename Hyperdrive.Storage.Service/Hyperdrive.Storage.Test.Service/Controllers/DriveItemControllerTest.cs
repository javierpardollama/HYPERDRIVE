using System.Net.Http.Headers;
using System.Net.Http.Json;
using Hyperdrive.Storage.Application.ViewModels.Additions;
using Hyperdrive.Storage.Application.ViewModels.Filters;
using Hyperdrive.Storage.Application.ViewModels.Updates;
using Hyperdrive.Storage.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hyperdrive.Storage.Test.Service.Controllers;

[TestFixture]
public class DriveItemControllerTest : BaseControllerTest
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:8061/api/storage/v1/driveitem/") };

    private ViewDriveItem Archive { get; set; }

    [SetUp]
    public new void SetUp()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
    }

    [Test]
    public async Task FindPaginatedDriveItemByApplicationUserId()
    {
        var content = JsonContent.Create(new FilterPageDriveItem { Index = 0, Size = 20, ApplicationUserId = User.Id });

        var response = await Client.PostAsync("page", content);
        response.EnsureSuccessStatusCode();
        var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItem>>();

        Assert.Pass();
    }

    [Test]
    public async Task FindPaginatedSharedDriveItemByApplicationUserId()
    {
        var content = JsonContent.Create(new FilterPageDriveItem { Index = 0, Size = 20, ApplicationUserId = User.Id });

        var response = await Client.PostAsync("page/shared", content);
        response.EnsureSuccessStatusCode();
        var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItem>>();

        Assert.Pass();
    }

    [Test]
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

    [Test, DependsOnTest("AddParentDriveItem")]
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


    [Test, DependsOnTest("UpdateDriveItemName")]
    public async Task RemoveDriveItemById()
    {
        var response = await Client.DeleteAsync($"remove/{Archive.Id}");
        response.EnsureSuccessStatusCode();

        Assert.Pass();
    }
}
