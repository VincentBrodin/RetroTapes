using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Customer> _customerRepo;

        public IndexModel(IRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public List<Customer> Customer { get;set; } = [];

        public void  OnGet()
        {
            Customer = _customerRepo.All().ToList();
        }
    }
}
