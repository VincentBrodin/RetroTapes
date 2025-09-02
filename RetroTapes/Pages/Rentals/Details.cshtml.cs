using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Pages.Rentals
{
    public class DetailsModel : PageModel
    {
        private readonly SakilaContext _context;

        public DetailsModel(SakilaContext context)
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
    }
}
