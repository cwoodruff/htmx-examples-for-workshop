using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.BulkUpdate;

[ValidateAntiForgeryToken]
public class Index(IContactService service) : PageModel
{
    public List<Contact>? ContactTableRows { get; set; }

    public void OnGet()
    {
        ContactTableRows = service.Get().ToList();
    }

    public PartialViewResult OnPutActivate(int[] ids)
    {
        if (ids != null && ids.Length > 0)
        {
            foreach (var id in ids)
            {
                // In a real application, you would verify that the current user 
                // has permission to update these specific records.
                service.Update(id, true);
            }
        }

        var models = service.Get();
        foreach (var m in models)
        {
            m.Updated = ids != null && ids.Contains(m.Id);
        }

        return Partial("_tbody", models.ToList());
    }

    public PartialViewResult OnPutDeactivate(int[] ids)
    {
        if (ids != null && ids.Length > 0)
        {
            foreach (var id in ids)
            {
                // In a real application, you would verify that the current user 
                // has permission to update these specific records.
                service.Update(id, false);
            }
        }

        var models = service.Get();
        foreach (var m in models)
        {
            m.Updated = ids != null && ids.Contains(m.Id);
        }

        return Partial("_tbody", models.ToList());
    }
}