using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DeleteRow;

public class IndexModel : PageModel
{
    IContactService contactService;

    public IList<Contact>? Contacts { get; set; }
    [FromQuery(Name = "Id")] public int Id { get; set; }

    public IndexModel(IContactService contactService)
    {
        this.contactService = contactService;
    }

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