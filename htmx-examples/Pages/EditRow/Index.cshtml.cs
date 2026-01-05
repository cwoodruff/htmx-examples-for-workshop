using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.EditRow;

[ValidateAntiForgeryToken]
public class IndexModel(IContactService contactService) : PageModel
{
    public IList<Contact>? Contacts { get; set; }

    [FromQuery(Name = "Id")] public int Id { get; set; }

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

    public PartialViewResult OnPutUpdate([FromForm] Contact contact)
    {
        contactService.Update(contact);

        return Partial("_TableRow", contact);
    }
}