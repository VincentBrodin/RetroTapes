using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class CreateModel : PageModel
    {

        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Address> _addressRepo;
        private readonly IRepository<Store> _storeRepo;

        public CreateModel(IRepository<Customer> customerRepo, IRepository<Address> addressRepo, IRepository<Store> storeRepo)
        {
            _customerRepo = customerRepo;
            _addressRepo = addressRepo;
            _storeRepo = storeRepo;
        }
        public IActionResult OnGet()
        {
            ViewData["AddressId"] = new SelectList(_addressRepo.All(), "AddressId", "AddressId");
            ViewData["StoreId"] = new SelectList(_storeRepo.All(), "StoreId", "StoreId");
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"Non valid state in edit customer");
                // return Page();
            }

            Customer.CreateDate = DateTime.Now;
            Customer.LastUpdate = DateTime.Now;

            _customerRepo.Add(Customer);
            _customerRepo.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
