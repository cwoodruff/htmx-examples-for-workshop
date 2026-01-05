using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.FileUpload;

[ValidateAntiForgeryToken]
public class IndexModel : PageModel
{
    private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".txt", ".doc", ".docx" };

    public void OnGet()
    {
    }

    [BindProperty, Display(Name = "File")] public IFormFile? UploadedFile { get; set; }

    public async Task<IActionResult> OnPostUpload()
    {
        if (!ValidateFile(UploadedFile))
        {
            ModelState.AddModelError(string.Empty, "Invalid file. Please upload a valid file under 10MB.");
            return BadRequest(ModelState);
        }

        await Task.Delay(1200);

        return Partial("_javascript", UploadedFile);
    }

    public async Task<IActionResult> OnPostUpload2()
    {
        if (!ValidateFile(UploadedFile))
        {
            ModelState.AddModelError(string.Empty, "Invalid file. Please upload a valid file under 10MB.");
            return BadRequest(ModelState);
        }

        await Task.Delay(1200);
        return Partial("_hyperscript", UploadedFile);
    }

    private static bool ValidateFile(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return false;

        if (file.Length > MaxFileSize)
            return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            return false;

        // Basic magic number check for common types
        try
        {
            using var stream = file.OpenReadStream();
            var buffer = new byte[4];
            stream.Read(buffer, 0, 4);

            return extension switch
            {
                ".jpg" or ".jpeg" => buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF,
                ".png" => buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47,
                ".pdf" => buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46,
                ".gif" => buffer[0] == 0x47 && buffer[1] == 0x49 && buffer[2] == 0x46,
                _ => true // Allow others but ideally we'd check all
            };
        }
        catch
        {
            return false;
        }
    }
}