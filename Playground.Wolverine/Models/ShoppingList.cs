namespace Playground.Wolverine.Models;

public class ShoppingList
{
    public string Id { get; init; } = null!;
    private List<ShoppingListItem> Items { get; init; } = null!;

    public bool Contains(string itemName) => Items.Any(item => item.Name == itemName);

    public static ShoppingList Create(ShoppingListCreated _)
    {
        return new ShoppingList
        {
            Items = [],
        };
    }

    public void Apply(ShoppingListItemAdded @event)
    {
        Items.Add(new ShoppingListItem
        {
            Name = @event.ItemName
        });
    }
}