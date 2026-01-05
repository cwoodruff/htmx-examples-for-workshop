using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace htmx_examples.Pages.ActiveSearch;

[ValidateAntiForgeryToken]
public class IndexModel(IHttpClientFactory factory) : PageModel
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    public void OnGet()
    {
    }

    [BindProperty] public string SearchText { get; set; }
    public List<Country> Countries { get; set; }

    public async Task<PartialViewResult> OnPostSearch()
    {
        Countries = new();
        try
        {
            var response = await _httpClient.GetAsync($"https://restcountries.com/v3.1/name/{SearchText}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JsonArray.Parse(result);
                if (json != null)
                {
                    foreach (var country in json.AsArray())
                    {
                        var name = country?["name"]?["common"]?.ToString();
                        if (name != null)
                        {
                            this.Countries.Add(new(name));
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            // Log error if necessary, but for this example we'll just return the empty list
        }

        return Partial("_searchResult", Countries);
    }
}