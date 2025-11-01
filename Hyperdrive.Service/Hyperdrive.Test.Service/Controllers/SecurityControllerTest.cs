using Hyperdrive.Application.ViewModels.Security;
using Hyperdrive.Application.ViewModels.Views;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Test.Service.Controllers
{
    [TestFixture]
    public class SecurityControllerTest : BaseControllerTest
    {
        private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:7297/api/v1/") };

        [Test, Order(1)]
        public async Task ChangePassword()
        {
            var content = JsonContent.Create(new SecurityPasswordChange
            {
                ApplicationUserId = User.Id, 
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                CurrentPassword = OldPassWord, NewPassword = NewPassWord
            });

            var response = await Client.PutAsync("password/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task RollbackPassword()
        {
            var content = JsonContent.Create(new SecurityPasswordChange
                { 
                    ApplicationUserId = User.Id,
                    ApplicationUserRefreshToken = User.RefreshToken.Value,
                    CurrentPassword = NewPassWord, 
                    NewPassword = OldPassWord 
                });

            var response = await Client.PutAsync("password/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(3)]
        public async Task ChangeEmail()
        {
            var content = JsonContent.Create(new SecurityEmailChange
            {
                ApplicationUserId = User.Id,
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                NewEmail = NewEmail
            });

            var response = await Client.PutAsync("email/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(4)]
        public async Task RollbackEmail()
        {
            var content = JsonContent.Create(new SecurityEmailChange
            {
                ApplicationUserId = User.Id,
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                NewEmail = OldEmail
            });

            var response = await Client.PutAsync("email/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(5)]
        public async Task ChangePhoneNumber()
        {
            var content = JsonContent.Create(new SecurityPhoneNumberChange
            {
                ApplicationUserId = User.Id,
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                NewPhoneNumber = "19830324"
            });

            var response = await Client.PutAsync("phonenumber/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(6)]
        public async Task ChangeName()
        {
            var content = JsonContent.Create(new SecurityNameChange
            {
                ApplicationUserId = User.Id,
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                NewFirstName = "Lora",
                NewLastName = "Baines"
            });

            var response = await Client.PutAsync("applicationuser/name/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }

        [Test, Order(7)]
        public async Task RollbackName()
        {
            var content = JsonContent.Create(new SecurityNameChange
            {
                ApplicationUserId = User.Id,
                ApplicationUserRefreshToken = User.RefreshToken.Value,
                NewFirstName = "Quorra",
                NewLastName = "Flynn"
            });

            var response = await Client.PutAsync("applicationuser/name/change", content);
            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

            Assert.Pass();
        }
    }
}
