using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Film> _filmRepo;
        private readonly IRepository<Language> _languageRepo;

        public CreateModel(IRepository<Film> filmRepo, IRepository<Language> languageRepo)
        {
            _filmRepo = filmRepo;
            _languageRepo = languageRepo;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var languages = await _languageRepo.AllAsync();
            ViewData["LanguageId"] = new SelectList(languages, "LanguageId", "Name");
            ViewData["OriginalLanguageId"] = new SelectList(languages, "LanguageId", "Name");
            return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _filmRepo.AddAsync(Film);
            await _filmRepo.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
