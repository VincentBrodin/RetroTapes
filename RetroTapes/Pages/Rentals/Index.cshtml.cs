using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;

namespace RetroTapes.Pages.Rentals
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Rental> _rentalRepository;

        public IndexModel(IRepository<Rental> rentalRepository)
        {
            _rentalRepository = rentalRepository;
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

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;

            var query = await _rentalRepository.AllAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                var term = $"%{Search.Trim()}%";
                query = query.Where(r =>

                (r.Inventory != null &&
                r.Inventory.Film != null &&
                EF.Functions.Like(r.Inventory.Film.Title, term))
                ||
                (r.Customer != null &&
                EF.Functions.Like(
                    (r.Customer.FirstName ?? "") + " " + (r.Customer.LastName ?? ""), term))
                );
            }

            if (ShowActiveRentals)
            {
                query = query.Where(r => r.ReturnDate == null);
            }

            int totalCount = query.Count();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Rentals = query
                .OrderByDescending(r => r.RentalDate)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }

}
