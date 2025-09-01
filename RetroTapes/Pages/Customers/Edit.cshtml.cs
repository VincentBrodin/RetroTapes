using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Address> _addressRepo;
        private readonly IRepository<Store> _storeRepo;

        public EditModel(IRepository<Customer> customerRepo, IRepository<Address> addressRepo, IRepository<Store> storeRepo)
        {
            _customerRepo = customerRepo;
            _addressRepo = addressRepo;
            _storeRepo = storeRepo;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerRepo.Get(id ?? -1);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            ViewData["AddressId"] = new SelectList(_addressRepo.All(), "AddressId", "AddressId");
            ViewData["StoreId"] = new SelectList(_storeRepo.All(), "StoreId", "StoreId");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"Non valid state in edit customer");
                // return Page();
            }

            _customerRepo.Update(Customer);

            try
            {
                _customerRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
            return _customerRepo.Get(id) != null;
        }
    }
}
