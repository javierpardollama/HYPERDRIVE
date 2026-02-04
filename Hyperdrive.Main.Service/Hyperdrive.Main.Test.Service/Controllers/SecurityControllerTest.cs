using Hyperdrive.Main.Application.ViewModels.Security;
using Hyperdrive.Main.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Test.Service.Controllers;

[TestFixture]
public class SecurityControllerTest : BaseControllerTest
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:55897/api/v1/security/") };

    [SetUp]
    public new void SetUp()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
    }

    [Test, Order(1)]
    public async Task ChangePassword()
    {
        var content = JsonContent.Create(new SecurityPasswordChange
        {
            ApplicationUserId = User.Id,
            CurrentPassword = OldPassWord,
            NewPassword = NewPassWord
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
            NewFirstName = "Lora",
            NewLastName = "Baines"
        });

        var response = await Client.PutAsync("name/change", content);
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
            NewFirstName = "Quorra",
            NewLastName = "Flynn"
        });

        var response = await Client.PutAsync("name/change", content);
        response.EnsureSuccessStatusCode();
        User = await response.Content.ReadFromJsonAsync<ViewApplicationUser>();

        Assert.Pass();
    }
}
