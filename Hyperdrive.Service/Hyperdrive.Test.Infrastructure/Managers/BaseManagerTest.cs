using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Contexts;
using Hyperdrive.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hyperdrive.Test.Infrastructure.Managers;

/// <summary>
///     Represents a <see cref="BaseManagerTest" /> class.
/// </summary>
public abstract class BaseManagerTest
{
    /// <summary>
    ///     Gets or Sets <see cref="IOptions{ApiSettings}" />
    /// </summary>
    protected IOptions<JwtSettings> ApiOptions { get; set; } = Options.Create(new JwtSettings
    {
        JwtAudiences = ["https://localhost:4200"],
        JwtAuthority = "https://localhost:7297",
        JwtIssuer = "https://localhost:7297",
        JwtExpireDays = 2,
        JwtExpireMinutes = 15,
        JwtKey = "I've got a bad feeling about this"
    });

    /// <summary>
    ///     Gets or Sets <see cref="ApplicationContext" />
    /// </summary>
    protected ApplicationContext Context { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="DbContextOptionsBuilder{ApplicationContext}" />
    /// </summary>
    protected DbContextOptionsBuilder<ApplicationContext> ContextOptionsBuilder { get; set; } =
        new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase("hyperdrive.db")
            .AddInterceptors(new SoftDeleteInterceptor());

    /// <summary>
    ///     Sets Up Context
    /// </summary>
    protected void SetUpContext()
    {
        Context = new ApplicationContext(ContextOptionsBuilder.Options);
    }
}