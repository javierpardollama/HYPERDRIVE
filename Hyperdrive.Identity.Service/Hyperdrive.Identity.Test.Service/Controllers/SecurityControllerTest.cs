using System.Net.Http.Headers;
using System.Net.Http.Json;
using Hyperdrive.Identity.Application.ViewModels.Security;
using Hyperdrive.Identity.Application.ViewModels.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hyperdrive.Identity.Test.Service.Controllers;

[TestFixture]
public class SecurityControllerTest : BaseControllerTest
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://localhost:8071/api/v1/security/") };

    [SetUp]
    public new void SetUp()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, User.Token.Value);
    }

    [Test]
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

    [Test, DependsOnTest("ChangePassword")]
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

    [Test, DependsOnTest("RollbackPassword")]
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

    [Test, DependsOnTest("ChangeEmail")]
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

    [Test, DependsOnTest("RollbackEmail")]
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

    [Test, DependsOnTest("ChangePhoneNumber")]
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

    [Test, DependsOnTest("ChangeName")]
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
