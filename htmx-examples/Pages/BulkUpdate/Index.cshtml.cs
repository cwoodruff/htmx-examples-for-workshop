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

    public IActionResult OnPutActivate(int[] ids)
    {
        if (ids != null && ids.Length > 0)
        {
            // Limit bulk operations to prevent abuse
            if (ids.Length > 100)
            {
                ModelState.AddModelError("", "Cannot update more than 100 records at once");
                return BadRequest(ModelState);
            }

            foreach (var id in ids)
            {
                // Verify the contact exists before updating
                var contact = service.GetById(id);
                if (contact == null)
                {
                    ModelState.AddModelError("", $"Contact with ID {id} not found");
                    return NotFound(ModelState);
                }

                // In a production application, add authorization checks here:
                // if (!User.IsInRole("Admin") && contact.OwnerId != User.GetUserId())
                // {
                //     return Forbid();
                // }

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

    public IActionResult OnPutDeactivate(int[] ids)
    {
        if (ids != null && ids.Length > 0)
        {
            // Limit bulk operations to prevent abuse
            if (ids.Length > 100)
            {
                ModelState.AddModelError("", "Cannot update more than 100 records at once");
                return BadRequest(ModelState);
            }

            foreach (var id in ids)
            {
                // Verify the contact exists before updating
                var contact = service.GetById(id);
                if (contact == null)
                {
                    ModelState.AddModelError("", $"Contact with ID {id} not found");
                    return NotFound(ModelState);
                }

                // In a production application, add authorization checks here:
                // if (!User.IsInRole("Admin") && contact.OwnerId != User.GetUserId())
                // {
                //     return Forbid();
                // }

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