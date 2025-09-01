using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRepository<Actor> _actorRepository;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger, IRepository<Actor> actorRepository)
    {
        _logger = logger;
        _actorRepository = actorRepository;
    }

    public void OnGet()
    {
        FirstName = _actorRepository.All().First().FirstName;
        LastName = _actorRepository.All().First().LastName;
    }
}