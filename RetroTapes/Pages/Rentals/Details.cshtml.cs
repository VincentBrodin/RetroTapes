using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;

namespace RetroTapes.Pages.Rentals
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Rental> _rentalRepo;

        public DetailsModel(SakilaContext context, IRepository<Rental> rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public Rental Rental { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Rental? rental = await _rentalRepo.GetAsync(id);

            if (rental == null)
                return NotFound();

            Rental = rental;
            return Page();
        }
    }
}
