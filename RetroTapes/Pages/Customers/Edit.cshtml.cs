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

        public async Task<IActionResult> OnGetAsync(int? id)
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
            ViewData["AddressId"] = new SelectList(await _addressRepo.AllAsync(), "AddressId", "Address1");
            ViewData["StoreId"] = new SelectList(await _storeRepo.AllAsync(), "StoreId", "StoreId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"Non valid state in edit customer");
                // return Page();
            }

            await _customerRepo.UpdateAsync(Customer);

            try
            {
                await _customerRepo.SaveChangesAsync();
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
            return _customerRepo.Find(c => c.CustomerId == id).FirstOrDefault() != null;
        }
    }
}
