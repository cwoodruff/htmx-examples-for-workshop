using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.LazyLoading;

public class IndexModel : PageModel
{
    public IndexModel()
    {
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnGetSalesChart()
    {
        // Simulate a slow database or API call
        await Task.Delay(2000);
        return Partial("_SalesChart");
    }

    public async Task<IActionResult> OnGetRecentActivity()
    {
        // Simulate another slow call
        await Task.Delay(1000);
        return Partial("_RecentActivity");
    }
}