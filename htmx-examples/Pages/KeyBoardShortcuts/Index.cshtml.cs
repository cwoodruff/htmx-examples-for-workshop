using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.KeyBoardShortcuts;

public class Index : PageModel
{
    public string Message { get; set; } = "Press a shortcut key!";

    public void OnGet()
    {
    }

    public IActionResult OnPostAction1()
    {
        return Content("<div class='alert alert-success'>Action 1 triggered via <strong>Alt+1</strong>!</div>");
    }

    public IActionResult OnPostAction2()
    {
        return Content("<div class='alert alert-info'>Action 2 triggered via <strong>Alt+2</strong>!</div>");
    }

    public IActionResult OnPostAction3()
    {
        return Content("<div class='alert alert-warning'>Action 3 triggered via <strong>Alt+3</strong>!</div>");
    }

    public IActionResult OnPostSearch(string q)
    {
        return Content($"<div class='alert alert-primary'>Search for '<strong>{q}</strong>' triggered via <strong>/</strong> key!</div>");
    }
}
