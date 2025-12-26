using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.ActiveSearch;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    public void OnGet()
    {
    }

    [BindProperty] public string SearchText { get; set; }
    public List<Country> Countries { get; set; }

    [ValidateAntiForgeryToken]
    public async Task<PartialViewResult> OnPostSearch()
    {
        Countries = new();
        var result = await _httpClient.GetStringAsync($"https://restcountries.com/v3.1/name/{SearchText}");
        //var jsonstr = await result.Content.ReadAsStringAsync();
        var json = JsonArray.Parse(result);
        foreach (var country in json.AsArray())
        {
            this.Countries.Add(new(country["name"]["common"].ToString()));
        }

        return Partial("_searchResult", Countries);
    }
}