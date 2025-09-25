using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;
            var allCustomers = Filter.Run(await _customerRepo.AllAsync());
            TotalPages = (int)Math.Ceiling(allCustomers.Count() / (double)PageSize);
            customers = allCustomers.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        // to Active or deactivate the customer
        public async Task<IActionResult> OnPostToggleActiveAsync(int id)
        {
            var customer = await _customerRepo.GetAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Active = customer.Active == "1" ? "0" : "1"; 
            await _customerRepo.UpdateAsync(customer);
            await _customerRepo.SaveChangesAsync();

            return RedirectToPage();
        }



    }
}
