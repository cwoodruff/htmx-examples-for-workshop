using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DialogBrowser;

public class Index : PageModel
{
    public string? Message { get; set; }
    public string? PromptResult { get; set; }

    public void OnGet()
    {
    }

    /// <summary>
    /// Handler for the confirmation dialog demo.
    /// This is called when the user confirms the action in the dialog.
    /// </summary>
    public IActionResult OnGetConfirmAction()
    {
        Message = "Action confirmed at " + DateTime.Now.ToLongTimeString();
        // Returns a partial or just a string that will be swapped into the target element
        return Content($"<div class='alert alert-success'>Successfully confirmed! (Server time: {DateTime.Now.ToLongTimeString()})</div>");
    }

    /// <summary>
    /// Handler for the prompt dialog demo.
    /// This receives the value entered by the user in the prompt modal.
    /// </summary>
    /// <param name="promptValue">The value from the input field in the modal</param>
    public IActionResult OnGetPromptAction(string promptValue)
    {
        return Content(string.IsNullOrEmpty(promptValue) ? "<div class='alert alert-warning'>You didn't enter anything in the prompt.</div>" : $"<div class='alert alert-info'>You entered: <strong>{promptValue}</strong></div>");
    }

    /// <summary>
    /// Returns the HTML for the confirmation modal.
    /// htmx will fetch this and swap it into a modal container on the page.
    /// </summary>
    public IActionResult OnGetConfirmModal()
    {
        return Partial("_ConfirmModal");
    }

    /// <summary>
    /// Returns the HTML for the prompt modal.
    /// </summary>
    public IActionResult OnGetPromptModal()
    {
        return Partial("_PromptModal");
    }
}
