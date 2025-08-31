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
            case "RentalRate":
                output = Desc() ? output.OrderByDescending(f => f.RentalDuration) : output.OrderBy(f => f.RentalRate);
                break;
            case "Length":
                output = Desc() ? output.OrderByDescending(f => f.Length) : output.OrderBy(f => f.Length);
                break;

            case "Language":
                output = Desc() ? output.OrderByDescending(f => f.Language) : output.OrderBy(f => f.Language);
                break;
        }
        return output;
    }

    bool Desc()
    {
        return OrderBy == "Descending";
    }
}
