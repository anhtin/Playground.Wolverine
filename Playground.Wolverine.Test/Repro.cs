using FluentAssertions;
using JasperFx.Core;
using Playground.Wolverine.Handlers;
using Playground.Wolverine.Models;
using Wolverine.Tracking;

namespace Playground.Wolverine.Test;

public class Repro : TestContext
{
    public Repro(AppFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Create_shopping_list()
    {
        // Act
        var (_, shoppingListId) = await Host.InvokeMessageAndWaitAsync<string>(new CreateShoppingList());

        // Assert
        var session = Store.LightweightSession();
        var appendedEvents = session.Events.QueryAllRawEvents().Where(x => x.StreamKey == shoppingListId).ToList();
        appendedEvents.Select(x => x.Data).Should().Equal(new ShoppingListCreated(shoppingListId));
    }

    [Fact]
    public async Task Add_shopping_list_item()
    {
        // Arrange
        var shoppingListId = CombGuidIdGeneration.NewGuid().ToString();
        var session = Store.LightweightSession();
        session.Events.StartStream<ShoppingList>(shoppingListId, new ShoppingListCreated(shoppingListId));
        await session.SaveChangesAsync();

        // Act
        var beforeAct = DateTimeOffset.UtcNow;
        await Host.InvokeMessageAndWaitAsync(new AddShoppingListItem(shoppingListId, "test-item"));

        // Assert
        var appendedEvents = session.Events.QueryAllRawEvents()
            .Where(x => x.StreamKey == shoppingListId)
            .Where(x => x.Timestamp > beforeAct)
            .ToList();
        appendedEvents.Select(x => x.Data).Should().Equal(new ShoppingListItemAdded("test-item"));
    }
}