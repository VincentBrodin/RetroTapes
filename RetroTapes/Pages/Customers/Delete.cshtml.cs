using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Customer> _customerRepo;

        public DeleteModel(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [BindProperty]
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

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepo.Get(id ?? -1);
            if (customer != null)
            {
                Customer = customer;
                _customerRepo.Delete(id ?? -1);
                _customerRepo.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
