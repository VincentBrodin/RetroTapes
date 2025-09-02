using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Pages.Rentals
{
    public class CreateModel : PageModel
    {
        private readonly SakilaContext _context;

        public CreateModel(SakilaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rental Rental { get; set; } = new Rental { RentalDate = DateTime.UtcNow };

        public SelectList Customers { get; private set; } = default!;
        public SelectList Inventories { get; private set; } = default!;
        public SelectList Staffs { get; private set; } = default!;

        public async Task OnGetAsync()
        {
            await LoadSelectListsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await ValidateAvailabilityAsync();

            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            _context.Rentals.Add(Rental);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Rental created.";
            return RedirectToPage("Details", new { id = Rental.RentalId });
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

            Customers = new SelectList(customers, "CustomerId", "Name");
            Inventories = new SelectList(inventories, "InventoryId", "Label");
            Staffs = new SelectList(staff, "StaffId", "Name");
        }

        private async Task ValidateAvailabilityAsync()
        {
            if (Rental.InventoryId == 0)
            {
                ModelState.AddModelError("Rental.InventoryId", "Please select a copy (inventory).");
                return;
            }

            // Ensure the selected copy isn't already out
            var activeLoanExists = await _context.Rentals
                .AsNoTracking()
                .AnyAsync(r => r.InventoryId == Rental.InventoryId && r.ReturnDate == null);

            if (activeLoanExists)
            {
                ModelState.AddModelError("Rental.InventoryId", "This copy is already rented out.");
            }

            if (Rental.ReturnDate.HasValue && Rental.ReturnDate.Value < Rental.RentalDate)
            {
                ModelState.AddModelError("Rental.ReturnDate", "Return date cannot be before rental date.");
            }
        }
    }
}
