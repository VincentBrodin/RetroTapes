using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;
using Microsoft.AspNetCore.Mvc;
using RetroTapes.Pages.Shared;

namespace RetroTapes.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Film> _filmRepo;

        public IndexModel(IRepository<Film> filmRepo)
        {
            _filmRepo = filmRepo;
        }

        [BindProperty(SupportsGet = true)]
        public FilterCriteria Filter { get; set; } = new FilterCriteria();

        public List<Film> Films { get; set; } = new();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 10;

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            var allFilms = Filter.Run(await _filmRepo.AllAsync());
            TotalPages = (int)Math.Ceiling(allFilms.Count() / (double)PageSize);
            Films = allFilms.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
