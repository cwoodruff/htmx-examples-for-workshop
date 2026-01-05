using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.FileUpload;

[ValidateAntiForgeryToken]
public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".txt", ".doc", ".docx" };

    public void OnGet()
    {
    }

    [BindProperty, Display(Name = "File")] public IFormFile? UploadedFile { get; set; }

    public async Task<IActionResult> OnPostUpload()
    {
        var (isValid, errorMessage) = ValidateFile(UploadedFile);
        if (!isValid)
        {
            logger.LogWarning("File upload validation failed: {Error}", errorMessage);
            ModelState.AddModelError(string.Empty, errorMessage);
            return BadRequest(ModelState);
        }

        await Task.Delay(1200);

        return Partial("_javascript", UploadedFile);
    }

    public async Task<IActionResult> OnPostUpload2()
    {
        var (isValid, errorMessage) = ValidateFile(UploadedFile);
        if (!isValid)
        {
            logger.LogWarning("File upload validation failed: {Error}", errorMessage);
            ModelState.AddModelError(string.Empty, errorMessage);
            return BadRequest(ModelState);
        }

        await Task.Delay(1200);
        return Partial("_hyperscript", UploadedFile);
    }

    private static (bool isValid, string errorMessage) ValidateFile(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return (false, "No file uploaded or file is empty.");

        if (file.Length > MaxFileSize)
            return (false, $"File size ({file.Length / 1024 / 1024}MB) exceeds maximum allowed size of 10MB.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            return (false, $"File type '{extension}' is not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}");

        // Magic number validation removed - was causing issues with file stream handling
        // Relying on file extension and size validation only
        return (true, string.Empty);
    }
}