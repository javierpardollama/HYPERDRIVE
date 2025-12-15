using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers;

public class DriveItemVersionControllerTest : BaseControllerTest
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:55897/api/v1/driveitem") };

    private ViewDriveItem Archive { get; set; }

    [SetUp]
    public new void SetUp()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
    }

    [Test, Order(1)]
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

    [Test, Order(2)]
    public async Task FindPaginatedDriveItemVersionByDriveItemId()
    {
        var content = JsonContent.Create(new FilterPageDriveItemVersion
        {
            Index = 0,
            Size = 20,
            Id = Archive.Id,
        });

        var response = await Client.PostAsync($"version/page", content);
        response.EnsureSuccessStatusCode();
        var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItemVersion>>();

        Assert.Pass();
    }

    [Test, Order(3)]
    public async Task RemoveDriveItemById()
    {
        var response = await Client.DeleteAsync($"remove/{Archive.Id}");
        response.EnsureSuccessStatusCode();

        Assert.Pass();
    }

}
