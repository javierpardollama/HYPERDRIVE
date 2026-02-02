using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Hyperdrive.Ai.Application.Profiles;

/// <summary>
///     Represents a <see cref="CredentialProfile" /> class
/// </summary>
public static class CredentialProfile
{
    /// <summary>
    /// Transforms to Tuple
    /// </summary>
    /// <param name="request">Injected <see cref="HttpRequest" /></param>
    /// <returns>Instance of <see cref="Tuple{string, string}" /></returns>
    public static (string Name, string Password) ToTuple(this HttpRequest @request)
    {
        if (!@request.Headers.TryGetValue("Authorization", out var @headerValue))
            throw new InvalidOperationException("Authorization header is missing.");

        var @header = AuthenticationHeaderValue.Parse(@headerValue!);

        if (!nameof(AuthenticationSchemes.Basic).Equals(@header.Scheme, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Authorization scheme is not Basic.");

        if (string.IsNullOrWhiteSpace(@header.Parameter))
            throw new InvalidOperationException("Authorization header is missing credentials.");

        var @encoded = Convert.FromBase64String(@header.Parameter);

        var @parts = Encoding.UTF8.GetString(@encoded).Split(':', 2);

        if (parts is not [var @name, var @password])
            throw new InvalidOperationException("Invalid Basic Authentication credential format.");

        return (name, password);
    }
}
