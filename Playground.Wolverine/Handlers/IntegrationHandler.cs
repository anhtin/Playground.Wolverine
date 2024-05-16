using Marten.Events;
using Playground.Wolverine.Models;

namespace Playground.Wolverine.Handlers;

public static class IntegrationHandler
{
    public static bool WorksWithoutIEvent = false;
    public static bool WorksWithIEvent = false;

    public static void Handle(ShoppingListCreated _)
    {
        WorksWithoutIEvent = true;
    }

    public static void Handle(IEvent<ShoppingListCreated> _)
    {
        WorksWithIEvent = true;
    }
}