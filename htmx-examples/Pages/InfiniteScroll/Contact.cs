using System.ComponentModel;

namespace htmx_examples.Pages.InfiniteScroll;

public class Contact(string v1, string v2, Guid newGuid)
{
    public string? Name { get; set; } = v1;
    public string? Email { get; set; } = v2;
    [DisplayName("ID")] public string? UniqueIdentifier { get; set; } = newGuid.ToString();
}