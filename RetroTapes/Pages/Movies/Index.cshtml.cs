using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly SakilaContext _context;

        public IndexModel(SakilaContext context)
        {
            _context = context;
        }

        public IList<Film> Films { get; set; } = new List<Film>();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 10;

        public async Task OnGetAsync(int pageIndex = 1)
        {
            var totalFilms = await _context.Films.CountAsync();

            TotalPages = (int)Math.Ceiling(totalFilms / (double)PageSize);
            PageIndex = pageIndex;

            Films = await _context.Films
                .Include(f => f.Language)
                .OrderBy(f => f.Title)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
