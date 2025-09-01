using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Customer> _customerRepo;

        public DetailsModel(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public Customer Customer { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepo.Get(id ?? -1);

            if (customer is not null)
            {
                Customer = customer;

                return Page();
            }

            return NotFound();
        }
    }
}
