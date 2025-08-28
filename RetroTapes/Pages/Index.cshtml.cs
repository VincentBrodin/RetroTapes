using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Models;

namespace RetroTapes.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SakilaContext _sakilaContext;
    public string FirstName { get; set; } = "Default";

    public IndexModel(ILogger<IndexModel> logger, SakilaContext sakilaContext)
    {
        _logger = logger;
        _sakilaContext = sakilaContext;
    }

    public void OnGet()
    {
        Console.WriteLine("Writeline test");
        FirstName = "Updated default";
        FirstName = _sakilaContext.Actors.First().FirstName;
    }
}