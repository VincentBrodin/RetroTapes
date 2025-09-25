using Microsoft.AspNetCore.Mvc.RazorPages;
using RetroTapes.Models;

namespace RetroTapes.Pages.Shared
{
    public class _SortAndFilterCustomerModel : PageModel
    {
        public string SearchTerm { get; set; } = "";
        public string SortBy { get; set; } = "";
        public string OrderBy { get; set; } = "";
    }

    public class FilterCriteriaCustomer
    {
        public string SearchTerm { get; set; } = "";
        public string SortBy { get; set; } = "";
        public string OrderBy { get; set; } = "";

        public IEnumerable<Customer> Run(IEnumerable<Customer> customers)
        {
            // فلترة
            var output = customers.Where(c =>
                string.IsNullOrEmpty(SearchTerm) ||
                c.CustomerId.ToString().Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                c.FirstName.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                c.LastName.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                (c.Email?.Contains(SearchTerm, StringComparison.CurrentCultureIgnoreCase) ?? false)
            );

            // فرز
            switch (SortBy)
            {
                case "FirstName":
                    output = Desc() ? output.OrderByDescending(c => c.FirstName) : output.OrderBy(c => c.FirstName);
                    break;
                case "LastName":
                    output = Desc() ? output.OrderByDescending(c => c.LastName) : output.OrderBy(c => c.LastName);
                    break;
                case "Email":
                    output = Desc() ? output.OrderByDescending(c => c.Email) : output.OrderBy(c => c.Email);
                    break;
            }

            return output;
        }

        private bool Desc()
        {
            return OrderBy.Equals("Descending", StringComparison.OrdinalIgnoreCase);
        }
    }
}
