using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DialogCustom;

public class Index : PageModel
{
    public void OnGet()
    {
    }

    /// <summary>
    /// Handler for the confirmation action.
    /// </summary>
    public IActionResult OnGetConfirmAction()
    {
        return Content($"<div class='alert alert-success'>Custom Action confirmed at {DateTime.Now.ToLongTimeString()}</div>");
    }

    /// <summary>
    /// Handler for the prompt action.
    /// </summary>
    /// <param name="promptValue">The value from the input field in the custom modal</param>
    public IActionResult OnGetPromptAction(string promptValue)
    {
        return Content(string.IsNullOrEmpty(promptValue) 
            ? "<div class='alert alert-warning'>You didn't enter anything in the custom prompt.</div>" 
            : $"<div class='alert alert-info'>Custom Prompt result: <strong>{promptValue}</strong></div>");
    }

    /// <summary>
    /// Returns the HTML for the custom confirmation modal.
    /// </summary>
    public IActionResult OnGetConfirmModal()
    {
        return Partial("_ConfirmModal");
    }

    /// <summary>
    /// Returns the HTML for the custom prompt modal.
    /// </summary>
    public IActionResult OnGetPromptModal()
    {
        return Partial("_PromptModal");
    }
}
