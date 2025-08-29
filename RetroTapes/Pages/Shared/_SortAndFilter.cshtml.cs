using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Models;

namespace RetroTapes.Pages.Shared;

public class _SortAndFilter : PageModel
{
    public void OnGet()
    {
    }
}

public class FilterCriteria
{
    public string SearchTerm { get; set; } = "";
    public string SortBy { get; set; } = "";
    public string OrderBy { get; set; } = "";

    public IEnumerable<Film> Run(IEnumerable<Film> films)
    {
        var output = films.Where(f => string.IsNullOrEmpty(SearchTerm) || f.Title.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase));
        switch (SortBy)
        {
            case "Title":
                output = Desc() ? output.OrderByDescending(f => f.Title) : output.OrderBy(f => f.Title);
                break;
            case "ReleaseDate":
                output = Desc() ? output.OrderByDescending(f => int.TryParse(f.ReleaseYear, out var year) ? year : 0) : output.OrderBy(f => int.TryParse(f.ReleaseYear, out var year) ? year : 0);
                break;
            case "Rating":
                output = Desc() ? output.OrderByDescending(f => f.Rating) : output.OrderBy(f => f.Rating);
                break;
        }
        return output;
    }

    bool Desc()
    {
        return OrderBy == "Descending";
    }
}
