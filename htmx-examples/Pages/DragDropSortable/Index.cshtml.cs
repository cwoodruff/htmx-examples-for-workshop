using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DragDropSortable;

public class Index : PageModel
{
    public List<Item> Items { get; set; } = new();

    // In-memory store for demonstration purposes
    private static List<Item> _storedItems = new()
    {
        new Item { Id = 1, Name = "Item 1", Order = 1 },
        new Item { Id = 2, Name = "Item 2", Order = 2 },
        new Item { Id = 3, Name = "Item 3", Order = 3 },
        new Item { Id = 4, Name = "Item 4", Order = 4 },
        new Item { Id = 5, Name = "Item 5", Order = 5 }
    };

    public void OnGet()
    {
        Items = _storedItems.OrderBy(i => i.Order).ToList();
    }

    /// <summary>
    /// Handler for reordering items.
    /// Sortable.js sends the item IDs in the new order as multiple parameters with the same name.
    /// </summary>
    /// <param name="itemIds">The list of IDs in the new order</param>
    public IActionResult OnPostReorder(int[] itemIds)
    {
        if (itemIds != null)
        {
            for (int i = 0; i < itemIds.Length; i++)
            {
                var id = itemIds[i];
                var item = _storedItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Order = i + 1;
                }
            }
        }

        Items = _storedItems.OrderBy(i => i.Order).ToList();
        
        // Return only the list partial to update the UI
        return Partial("_ItemList", Items);
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
}
