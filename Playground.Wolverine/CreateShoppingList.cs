using Marten;

namespace Playground.Wolverine;

public record CreateShoppingList();

public static class CreateShoppingListHandler
{
    public static void Handle(CreateShoppingList command, IDocumentSession session)
    {
        session.Events.StartStream<ShoppingList>(new ShoppingListCreated());
    }
}

public class ShoppingList
{
}

public record ShoppingListCreated();
