using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DeleteRow;

[ValidateAntiForgeryToken]
public class IndexModel(IContactService contactService) : PageModel
{
    public IList<Contact>? Contacts { get; set; }
    [FromQuery(Name = "Id")] public int Id { get; set; }

    public void OnGet()
    {
        this.Contacts = contactService.Get().ToArray();
    }

    public IActionResult OnPostContact()
    {
        //contactService.Delete(this.Id);
        return new OkResult();
    }

    public IActionResult OnDeleteContact()
    {
        contactService.Delete(this.Id);
        return new OkResult();
    }
}