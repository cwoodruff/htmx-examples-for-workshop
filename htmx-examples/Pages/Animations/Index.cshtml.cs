using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.Animations;

public class IndexModel : PageModel
{
    private static readonly string[] Colors = { "red", "green", "blue", "orange", "purple", "cyan" };
    private static int _colorIndex = 0;

    public void OnGet()
    {
    }

    public IActionResult OnGetColorChange()
    {
        _colorIndex = (_colorIndex + 1) % Colors.Length;
        var color = Colors[_colorIndex];
        
        return Content($@"<div id=""color-demo"" 
                               hx-get=""/Animations/Index?handler=ColorChange"" 
                               hx-swap=""outerHTML"" 
                               style=""background-color: {color};"">
                            Click Me!
                          </div>", "text/html");
    }

    public IActionResult OnGetFadeOut()
    {
        return new EmptyResult();
    }

    public IActionResult OnGetNewContent()
    {
        return Content(@"<div class=""alert alert-success htmx-added"">
                            New content added at " + DateTime.Now.ToLongTimeString() + 
                        "</div>", "text/html");
    }

    public IActionResult OnGetFadeInContent()
    {
        return Content($@"<div class=""alert alert-success fade-in"">
                            Faded in at {DateTime.Now.ToLongTimeString()}
                          </div>", "text/html");
    }

    public IActionResult OnGetSlideContent()
    {
        return Content($@"<div class=""alert alert-info slide-it"">
                            Slid down at {DateTime.Now.ToLongTimeString()}
                          </div>", "text/html");
    }

    public IActionResult OnGetCrossfadeContent()
    {
        var isInitial = Request.Query["initial"] == "true";
        var content = isInitial ? "Initial Content" : $"New Content at {DateTime.Now.ToLongTimeString()}";
        var nextInitial = !isInitial;
        
        return Content($@"<div id=""crossfade-demo"" 
                               class=""crossfade-item alert {(isInitial ? "alert-secondary" : "alert-warning")}""
                               hx-get=""/Animations/Index?handler=CrossfadeContent&initial={nextInitial.ToString().ToLower()}"" 
                               hx-target=""#crossfade-demo"" 
                               hx-swap=""outerHTML swap:1s""
                               hx-trigger=""none"">
                            {content}
                          </div>", "text/html");
    }

    public IActionResult OnGetValidateShake(string testInput)
    {
        bool isInvalid = testInput?.ToLower() == "invalid";
        string shakeClass = isInvalid ? "shake" : "";
        string borderClass = isInvalid ? "is-invalid" : "";
        
        return Content($@"<div id=""shake-demo"">
                            <input type=""text"" class=""form-control mb-2 {shakeClass} {borderClass}"" 
                                   name=""testInput"" 
                                   value=""{testInput}""
                                   placeholder=""Type 'invalid'...""
                                   hx-get=""/Animations/Index?handler=ValidateShake"" 
                                   hx-target=""#shake-demo"" 
                                   hx-swap=""outerHTML""
                                   hx-trigger=""keyup[key=='Enter']"">
                            {(isInvalid ? "<div class='invalid-feedback d-block'>Invalid input detected!</div>" : "")}
                          </div>", "text/html");
    }

    public async Task<IActionResult> OnGetMorphAction()
    {
        await Task.Delay(2000); // Simulate work
        return new EmptyResult();
    }
}
