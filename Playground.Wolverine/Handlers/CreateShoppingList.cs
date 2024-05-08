using JasperFx.Core;
using Marten;
using Playground.Wolverine.Models;

namespace Playground.Wolverine.Handlers;

public record CreateShoppingList();

public static class CreateShoppingListHandler
{
    public static string Handle(CreateShoppingList _, IDocumentSession session)
    {
        var shoppingListId = CombGuidIdGeneration.NewGuid().ToString();
        session.Events.StartStream<ShoppingList>(shoppingListId, new ShoppingListCreated(shoppingListId));
        return shoppingListId;
    }
}