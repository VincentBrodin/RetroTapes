using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;

namespace RetroTapes.Pages.Rentals
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Rental> _rentalRepo;

        public IndexModel(IRepository<Rental> rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowActiveRentals { get; set; }

        public List<Rental> Rentals { get; set; } = new();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 10;

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public void OnGet(int pageIndex = 1)
        {
            PageIndex = pageIndex;

            // NOTE: RentalRepository.All() already includes Inventory->Film, Customer, Staff and uses AsNoTracking()
            var query = _rentalRepo.All();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var s = Search.Trim().ToLower();
                query = query.Where(r =>
                    (r.Inventory != null && r.Inventory.Film != null && r.Inventory.Film.Title.ToLower().Contains(s)) ||
                    (
                    (r.Customer != null) &&
                    ($"{r.Customer.FirstName}{r.Customer.LastName}".Contains(s))
                    )
                    );
            }

            if (ShowActiveRentals)
            {
                query = query.Where(r => r.ReturnDate == null);
            }

            var totalCount = query.Count();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Rentals = query
                .OrderByDescending(r => r.RentalDate)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
