using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.EditRow;

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

    public PartialViewResult OnGetEdit(int Id)
    {
        var contact = contactService.Get(Id);

        return Partial("_EditRow", contact);
    }

    public PartialViewResult OnGetView(int Id)
    {
        var contact = contactService.Get(Id);

        return Partial("_TableRow", contact);
    }

    [ValidateAntiForgeryToken]
    public PartialViewResult OnPut(Contact contact)
    {
        contactService.Update(contact);

        return Partial("_TableRow", contact);
    }
}