using Marten;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Playground.Wolverine.Test;

public class TestContext : IClassFixture<AppFixture>
{
    protected TestContext(AppFixture fixture)
    {
        Host = fixture.Host;
        Store = Host.Services.GetRequiredService<IDocumentStore>();
    }

    protected IHost Host { get; }

    protected IDocumentStore Store { get; }
}