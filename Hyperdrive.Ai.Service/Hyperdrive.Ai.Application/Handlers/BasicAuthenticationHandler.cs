using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Domain.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Application.Handlers;

/// <summary>
/// Represents a <see cref="BasicAuthenticationHandler" /> class. Inherits <see cref="AuthenticationHandler{AuthenticationSchemeOptions}" />
/// </summary>
/// <param name="options">Injected <see cref="IOptionsMonitor{AuthenticationSchemeOptions}" /></param>
/// <param name="logger">Injected <see cref="ILoggerFactory" /></param>
/// <param name="encoder">Injected <see cref="UrlEncoder" /></param>
/// <param name="credentialManager">Injected <see cref="ICredentialManager" /></param>
public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ICredentialManager credentialManager) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    /// <summary>
    /// Gets Authentication Ticket
    /// </summary>
    /// <param name="@credentials">Injected <see cref="Tuple{string, string}" /></param>
    /// <returns>Instance of <see cref="AuthenticationTicket" /></returns>
    private AuthenticationTicket CreateTicket((string Name, string Password) @credentials)
    {
        List<Claim> @claims =
        [
            new(ClaimTypes.Name, @credentials.Name),
            new(ClaimTypes.AuthenticationInstant, DateTime.Now.ToString()),
            new(ClaimTypes.Locality, CultureInfo.CurrentCulture.TwoLetterISOLanguageName),
            new(ClaimTypes.Version, Environment.OSVersion.VersionString),
            new(ClaimTypes.System, Environment.MachineName)
        ];

        return new AuthenticationTicket(
            new ClaimsPrincipal(new ClaimsIdentity(@claims,
                Scheme.Name)),
            Scheme.Name);
    }

    /// <summary>
    /// Handles Authentication Asynchronously
    /// </summary>
    /// <returns>Instance of <see cref="AuthenticateResult" /></returns>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        (string Name, string Password) credentials;

        try
        {
            credentials = Request.ToTuple();
        }
        catch (Exception)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));
        }

        return credentialManager.CanAuthenticate(credentials.Name, credentials.Password)
            ? Task.FromResult(AuthenticateResult.Success(CreateTicket(credentials)))
            : Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));
    }

}