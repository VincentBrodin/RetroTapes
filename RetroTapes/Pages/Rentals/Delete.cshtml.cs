using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Pages.Rentals
{
    public class DeleteModel : PageModel
    {
        private readonly SakilaContext _context;

        public DeleteModel(SakilaContext context)
        {
            _context = context;
        }

        public Rental Rental { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null) return NotFound();

            Rental = rental;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id);
            if (rental == null) return NotFound();

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Rental deleted.";
            return RedirectToPage("Index");
        }
    }
}

