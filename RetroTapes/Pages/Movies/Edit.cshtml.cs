using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Film> _filmRepo;
        private readonly IRepository<Language> _languageRepo;

        public EditModel(IRepository<Film> filmRepo, IRepository<Language> langaugeRepo)
        {
            _filmRepo = filmRepo;
            _languageRepo = langaugeRepo;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var films = await _filmRepo.FindAsync(f => f.FilmId == id);
            var film = films.FirstOrDefault();
            if (film == null)
            {
                return NotFound();
            }
            Film = film;
            var languages = await _languageRepo.AllAsync();
            ViewData["LanguageId"] = new SelectList(languages, "LanguageId", "Name");
            ViewData["OriginalLanguageId"] = new SelectList(languages, "LanguageId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                Console.WriteLine($"Non valid state in edit movie");
                // return Page();
            }

            await _filmRepo.UpdateAsync(Film);

            try
            {
                await _filmRepo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(Film.FilmId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FilmExists(int id)
        {
            var film = _filmRepo.Find(f => f.FilmId == id).FirstOrDefault();
            return film != null;
        }
    }
}
