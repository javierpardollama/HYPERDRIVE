using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers
{
    [TestFixture]
    public class DriveItemControllerTest : BaseControllerTest
    {
        private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7297/api/v1/driveitem/") };

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
        public async Task AddDriveItem()
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

            var response = await Client.PostAsync("up", content);
            response.EnsureSuccessStatusCode();
            Archive = await response.Content.ReadFromJsonAsync<ViewDriveItem>();

            Assert.Pass();
        }

        [Test, Order(5)]
        public async Task FindAllDriveItemVersionByDriveItemId()
        {
            var response = await Client.DeleteAsync($"all/version/{Archive.Id}");
            response.EnsureSuccessStatusCode();
            var versions = await response.Content.ReadFromJsonAsync<IList<ViewDriveItemVersion>>();

            Assert.Pass();
        }

        [Test, Order(6)]
        public async Task RemoveDriveItemById()
        {
            var response = await Client.DeleteAsync($"remove/{Archive.Id}");
            response.EnsureSuccessStatusCode();

            Assert.Pass();
        }
    }
}
