using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.DialogUIKit;

public class Index : PageModel
{
    public string? Message { get; set; }

    public void OnGet()
    {
    }

    /// <summary>
    /// Handler for the UIKit confirmation dialog demo.
    /// </summary>
    public IActionResult OnGetConfirmAction()
    {
        return Content($"<div class='uk-alert-success' uk-alert><a class='uk-alert-close' uk-close></a><p>UIKit Action confirmed at {DateTime.Now.ToLongTimeString()}</p></div>");
    }

    /// <summary>
    /// Handler for the UIKit prompt dialog demo.
    /// </summary>
    public IActionResult OnGetPromptAction(string promptValue)
    {
        return Content(string.IsNullOrEmpty(promptValue) 
            ? "<div class='uk-alert-warning' uk-alert><a class='uk-alert-close' uk-close></a><p>You didn't enter anything in the UIKit prompt.</p></div>" 
            : $"<div class='uk-alert-primary' uk-alert><a class='uk-alert-close' uk-close></a><p>UIKit received: <strong>{promptValue}</strong></p></div>");
    }

    /// <summary>
    /// Returns the HTML for the UIKit confirmation modal.
    /// </summary>
    public IActionResult OnGetConfirmModal()
    {
        return Partial("_ConfirmModal");
    }

    /// <summary>
    /// Returns the HTML for the UIKit prompt modal.
    /// </summary>
    public IActionResult OnGetPromptModal()
    {
        return Partial("_PromptModal");
    }
}
