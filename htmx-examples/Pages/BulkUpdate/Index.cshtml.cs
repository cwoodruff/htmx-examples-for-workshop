using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.BulkUpdate;

public class Index(IContactService service) : PageModel
{
    public List<Contact>? ContactTableRows { get; set; }

    public void OnGet()
    {
        ContactTableRows = service.Get().ToList();
    }

    [ValidateAntiForgeryToken]
    public PartialViewResult OnPutActivate(int[] ids)
    {
        foreach (var id in ids)
            service.Update(id, true);
        var models = service.Get();
        foreach (var m in models)
            if (ids.Contains(m.Id))
                m.Updated = true;
            else m.Updated = false;
        return Partial("_tbody", models.ToList());
    }

    [ValidateAntiForgeryToken]
    public PartialViewResult OnPutDeactivate(int[] ids)
    {
        foreach (var id in ids)
            service.Update(id, false);
        var models = service.Get();
        foreach (var m in models)
            if (ids.Contains(m.Id))
                m.Updated = true;
            else m.Updated = false;

        return Partial("_tbody", models.ToList());
    }
}