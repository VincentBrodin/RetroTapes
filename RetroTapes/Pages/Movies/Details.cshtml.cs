using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Film> _filmRepo;

        public DetailsModel(SakilaContext context, IRepository<Film> filmRepo)
        {
            _filmRepo = filmRepo;
        }

        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film? film = await _filmRepo.GetAsync((int)id);

            if (film == null)
            {
                return NotFound();
            }

            Film = film;
            return Page();
        }
    }
}
