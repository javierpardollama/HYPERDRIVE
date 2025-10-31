using Hyperdrive.Application.ViewModels.Additions;
using Hyperdrive.Application.ViewModels.Filters;
using Hyperdrive.Application.ViewModels.Updates;
using Hyperdrive.Application.ViewModels.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers
{
    [TestFixture]
    public class ApplicationUserControllerTest : AuthControllerTest
    {
        private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7297/api/v1/") };

        private ViewApplicationRole Role { get; set; }

        [Test, Order(1)]
        public async Task FindAllApplicationRole()
        {
            var response = await Client.GetAsync("applicationuser/all");
            response.EnsureSuccessStatusCode();
            var roles = await response.Content.ReadFromJsonAsync<List<ViewCatalog>>();

            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task FindPaginatedApplicationRole()
        {
            var content = JsonContent.Create(new FilterPageApplicationUser { Index = 0, Size = 20, ApplicationUserId = User.Id });

            var response = await Client.PostAsync("applicationuser/page", content);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<ViewPage<ViewApplicationUser>>();

            Assert.Pass();
        }

        [Test, Order(3)]
        public async Task AddApplicationRole()
        {
            var content = JsonContent.Create(new AddApplicationRole { ApplicationUserId = User.Id, Name = "Rogue", ImageUri = "URL/Rogue_500px.png" });

            var response = await Client.PostAsync("create", content);
            response.EnsureSuccessStatusCode();
            Role = await response.Content.ReadFromJsonAsync<ViewApplicationRole>();

            Assert.Pass();
        }

        [Test, Order(4)]
        public async Task UpdateApplicationUser() 
        {
            var content = JsonContent.Create(new UpdateApplicationUser { ApplicationUserId = User.Id, ApplicationRoleNames = [ Role.Name ], Id = User.Id });

            var response = await Client.PutAsync("update", content);
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(5)]
        public async Task RemoveApplicationRoleById()
        {
            var response = await Client.DeleteAsync($"remove/{Role.Id}");
            response.EnsureSuccessStatusCode();

            Assert.Pass();
        }
    }
}
