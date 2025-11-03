using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers
{
    [TestFixture]
    public class DriveItemControllerTest: BaseControllerTest
    {
        private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7297/api/v1/driveitem/") };

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

        [Test, Order(1)]
        public async Task FindPaginatedSharedDriveItemByApplicationUserId()
        {
            var content = JsonContent.Create(new FilterPageDriveItem { Index = 0, Size = 20, ApplicationUserId = User.Id });

            var response = await Client.PostAsync("page/shared", content);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewDriveItem>>();

            Assert.Pass();
        }       
    }
}
