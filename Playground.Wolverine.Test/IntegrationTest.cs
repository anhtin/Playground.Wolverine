using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Playground.Wolverine.Handlers;
using Wolverine;

namespace Playground.Wolverine.Test;

public class IntegrationTest(AppFixture fixture) : TestContext(fixture)
{
    [Fact]
    public async Task Creating_shopping_list_publishes_ShoppingListCreatedIntegrationEvent()
    {
        // Act
        var messageBus = Host.Services.GetRequiredService<IMessageBus>();
        await messageBus.InvokeAsync<string>(new CreateShoppingList());

        // Assert
        Assert.Multiple(
            () => IntegrationHandler.WorksWithoutIEvent.Should().BeTrue(),
            () => IntegrationHandler.WorksWithIEvent.Should().BeTrue());
    }
}