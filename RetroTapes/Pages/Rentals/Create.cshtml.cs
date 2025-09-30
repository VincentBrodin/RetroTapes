using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;
using System.Text.Json;

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
        public Rental Rental { get; set; } = new Rental { RentalDate = DateTime.UtcNow, ReturnDate = null };
        public int SelectedId { get; set; }

        public SelectList Customers { get; private set; } = default!;
        public SelectList Inventories { get; private set; } = default!;
        public SelectList Staffs { get; private set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            SelectedId = id ?? 0;
            await LoadSelectListsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // await ValidateAvailabilityAsync();
            // if (!ModelState.IsValid)
            // {
            //     await LoadSelectListsAsync();
            //     return Page();
            // }

            Console.WriteLine(JsonSerializer.Serialize(Rental));

            Rental.LastUpdate = DateTime.UtcNow;

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

            var inventories = _context.Inventories
                .AsNoTracking()
                .Include(i => i.Film)
                .Include(i => i.Store)
                .ToList()
                .Where(i => SelectedId == 0 || i.FilmId == SelectedId)
                .Where(i => !i.Rentals.Any(r => r.ReturnDate == null))
                .Select(i => new
                {
                    i.InventoryId,
                    Label = i.Film!.Title + " (Store " + i.StoreId + ")"
                });

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

            var freeCopies = await _context.Rentals
                .AsNoTracking()
                .AnyAsync(r => r.InventoryId == Rental.InventoryId && r.ReturnDate != null && r.ReturnDate < DateTime.UtcNow);

            if (!freeCopies)
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
