# RetroTapes docs

## Components
### _SortAndFilter (RetroTapes.Pages.Shared)
A reusable search, filter, and sort UI for movie lists.
- Text search by movie title (`SearchTerm`)
- Sorting options (`SortBy`: Title, ReleaseDate & Rating)
- Order options (`OrderBy`: Ascending & Descending)
#### Usage
**1. Add a `FilterCriteria` property to your PageModel**
```csharp
[BindProperty(SupportsGet = true)]
public FilterCriteria Filter { get; set; } = new();
```
**2. Render the component in your page**
```razor
@await Html.PartialAsync("_SortAndFilter", Model.Filter)
```
**3. Apply the filter and sorting in your page logic**
```csharp
var films = Filter.Run(AllFilms).ToList();
```

#### Demo
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;
using RetroTapes.Pages.Shared;

namespace RetroTapes.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRepository<Film> _films;

    [BindProperty(SupportsGet = true)]
    public FilterCriteria Filter { get; set; } = new();
    public List<Film> Films { get; set; } = new();

    
    public IndexModel(ILogger<IndexModel> logger, IRepository<Film> films)
    {
        _logger = logger;
        _films = films;
    }

    public void OnGet()
    {
        Films = Filter.Run(_films.All()).ToList();
    }
}
```

```cshtml
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}

<div class="text-center">
    @await Html.PartialAsync("_SortAndFilter", Model.Filter)
    @foreach (var film in Model.Films)
    {
        <p>@film.Title - @film.Rating - @film.ReleaseYear</p>
    }
</div>
```
