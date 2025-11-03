using Hyperdrive.Application.ViewModels.Auth;
using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers
{
    public class BaseControllerTest
    {
        private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7297/api/v1/") };

        protected ViewApplicationUser User { get; set; }    
        
        protected string OldPassWord = "P@ssw0rd.0!d";

        protected string NewPassWord = "P@ssw0rd.n3w";

        protected string OldEmail = "quorra.flynn@encom.com";

        protected string NewEmail = "lora.baines@encom.com";

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var content = JsonContent.Create(new AuthJoinIn { Email = OldEmail, Password = OldPassWord });

            var response = await Client.PostAsync("auth/in", content);

            response.EnsureSuccessStatusCode();

            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [SetUp]
        public void SetUp() 
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
        }

        [TearDown]
        public async Task TearDown()
        {
            var content = JsonContent.Create(new SecurityRefreshTokenReset { ApplicationUserId = User.Id, ApplicationUserRefreshToken = User.RefreshToken.Value });

            var response = await Client.PutAsync("security/tokens/refresh", content);

            response.EnsureSuccessStatusCode();

            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var content = JsonContent.Create(new AuthSignOut { ApplicationUserId = User.Id, ApplicationUserRefreshToken = User.RefreshToken.Value });

            var response = await Client.PostAsync("auth/out", content);

            response.EnsureSuccessStatusCode();           

            Assert.Pass();
        }
    }
}
