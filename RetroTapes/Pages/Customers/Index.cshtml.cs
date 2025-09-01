using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Data;
using RetroTapes.Models;

namespace RetroTapes.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly RetroTapes.Data.SakilaContext _context;

        public IndexModel(RetroTapes.Data.SakilaContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.Store).ToListAsync();
        }
    }
}
