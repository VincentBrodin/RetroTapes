using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Film> _filmRepo;

        public DeleteModel(IRepository<Film> filmRepo)
        {
            _filmRepo = filmRepo;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _filmRepo.GetAsync(id ?? -1);

            if (film is not null)
            {
                Film = film;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _filmRepo.DeleteAsync(id ?? -1);
                await _filmRepo.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
