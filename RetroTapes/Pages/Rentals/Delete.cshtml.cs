using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Models;
using RetroTapes.Data;

namespace RetroTapes.Pages.Rentals
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Rental> _rentalRepo;

        public DeleteModel(IRepository<Rental> rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public Rental Rental { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var rental = await _rentalRepo.GetAsync(id);
            if (rental == null) return NotFound();
            Rental = rental;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var rental = await _rentalRepo.GetAsync(id);
            if (rental == null) return NotFound();
            await _rentalRepo.DeleteAsync(id);
            await _rentalRepo.SaveChangesAsync();
            TempData["StatusMessage"] = "Rental deleted.";
            return RedirectToPage("Index");
        }
    }
}

