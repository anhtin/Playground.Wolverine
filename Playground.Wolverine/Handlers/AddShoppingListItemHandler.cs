using Marten;
using Playground.Wolverine.Models;

namespace Playground.Wolverine.Handlers;

public record AddShoppingListItem(string ShoppingListId, string ItemName);

public static class AddShoppingListItemHandler
{
    public static async Task Handle(AddShoppingListItem command, IDocumentSession session)
    {
        var stream = await session.Events.FetchForWriting<ShoppingList>(command.ShoppingListId);
        var shoppingList = stream.Aggregate;
        if (shoppingList is null)
            throw new InvalidOperationException("Shopping list does not exist");

        if (shoppingList.Contains(command.ItemName))
            throw new InvalidOperationException("Item is already in shopping list");
        
        stream.AppendOne(new ShoppingListItemAdded(command.ItemName));
    }
}