using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.FileUpload;

public class IndexModel : PageModel
{
    private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".txt", ".doc", ".docx" };

    public void OnGet()
    {
    }

    [BindProperty, Display(Name = "File")] public IFormFile UploadedFile { get; set; }

    [ValidateAntiForgeryToken]
    public IActionResult OnPostUpload()
    {
        if (!ValidateFile(UploadedFile))
        {
            ModelState.AddModelError(string.Empty, "Invalid file. Please upload a valid file under 10MB.");
            return BadRequest(ModelState);
        }

        Task.Delay(1200);

        return Partial("_javascript", UploadedFile);
    }

    [ValidateAntiForgeryToken]
    public IActionResult OnPostUpload2()
    {
        if (!ValidateFile(UploadedFile))
        {
            ModelState.AddModelError(string.Empty, "Invalid file. Please upload a valid file under 10MB.");
            return BadRequest(ModelState);
        }

        Task.Delay(1200);
        return Partial("_hyperscript", UploadedFile);
    }

    private bool ValidateFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return false;

        if (file.Length > MaxFileSize)
            return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            return false;

        return true;
    }
}