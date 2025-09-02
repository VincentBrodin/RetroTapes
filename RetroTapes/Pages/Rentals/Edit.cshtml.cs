using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Pages.Rentals
{
    public class EditModel : PageModel
    {
        private readonly SakilaContext _context;

        public EditModel(SakilaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rental Rental { get; set; } = default!;

        public SelectList Customers { get; private set; } = default!;
        public SelectList Inventories { get; private set; } = default!;
        public SelectList Staffs { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .FirstOrDefaultAsync(r => r.RentalId == id);

            if (rental == null) return NotFound();

            Rental = rental;
            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await ValidateAsync();

            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            try
            {
                _context.Update(Rental);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Rental updated.";
                return RedirectToPage("Details", new { id = Rental.RentalId });
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Rentals.AnyAsync(r => r.RentalId == Rental.RentalId);
                if (!exists) return NotFound();

                ModelState.AddModelError(string.Empty, "This record was changed by someone else. Reload and try again.");
                await LoadSelectListsAsync();
                return Page();
            }
        }

        private async Task ValidateAsync()
        {
            if (Rental.ReturnDate.HasValue && Rental.ReturnDate.Value < Rental.RentalDate)
            {
                ModelState.AddModelError("Rental.ReturnDate", "Return date cannot be before rental date.");
            }

            // If inventory changed, ensure it's not already out
            var originalInventoryId = await _context.Rentals
                .AsNoTracking()
                .Where(r => r.RentalId == Rental.RentalId)
                .Select(r => r.InventoryId)
                .FirstOrDefaultAsync();

            if (originalInventoryId != Rental.InventoryId)
            {
                var activeLoanExists = await _context.Rentals
                    .AsNoTracking()
                    .AnyAsync(r => r.InventoryId == Rental.InventoryId && r.ReturnDate == null && r.RentalId != Rental.RentalId);

                if (activeLoanExists)
                {
                    ModelState.AddModelError("Rental.InventoryId", "This copy is already rented out.");
                }
            }
        }

        private async Task LoadSelectListsAsync()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
                .Select(c => new { c.CustomerId, Name = c.FirstName + " " + c.LastName })
                .ToListAsync();

            var inventories = await _context.Inventories
                .AsNoTracking()
                .Include(i => i.Film)
                .Include(i => i.Store)
                .Select(i => new
                {
                    i.InventoryId,
                    Label = i.Film!.Title + " (Store " + i.StoreId + ")"
                })
                .ToListAsync();

            var staff = await _context.Staff
                .AsNoTracking()
                .OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
                .Select(s => new { s.StaffId, Name = s.FirstName + " " + s.LastName })
                .ToListAsync();

            Customers = new SelectList(customers, "CustomerId", "Name", Rental?.CustomerId);
            Inventories = new SelectList(inventories, "InventoryId", "Label", Rental?.InventoryId);
            Staffs = new SelectList(staff, "StaffId", "Name", Rental?.StaffId);
        }
    }
}

