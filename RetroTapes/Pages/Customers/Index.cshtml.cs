using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;
using RetroTapes.Pages.Shared;

namespace RetroTapes.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Customer> _customerRepo;

        public IndexModel(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }


        [BindProperty(SupportsGet = true)]
        public FilterCriteriaCustomer Filter { get; set; } = new();

        public List<Customer> customers { get;set; } = new List<Customer>();

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        private const int PageSize = 10;

        public void OnGet(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            var allCustomers = Filter.Run(_customerRepo.All());
            TotalPages = (int)Math.Ceiling(allCustomers.Count() / (double)PageSize);
            customers = allCustomers.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
