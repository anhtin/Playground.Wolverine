using Alba;
using Microsoft.AspNetCore.Hosting;
using Oakton;
using Testcontainers.PostgreSql;

namespace Playground.Wolverine.Test;

public class AppFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

    public IAlbaHost Host { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        Host = await AlbaHost.For<Program>(webApp =>
        {
            OaktonEnvironment.AutoStartHost = true;
            webApp.UseEnvironment("Production");
            webApp.UseSetting("ConnectionStrings:Database", _dbContainer.GetConnectionString());
        });
    }

    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
        await _dbContainer.StopAsync();
    }
}