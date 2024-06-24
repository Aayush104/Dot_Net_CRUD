using DotNetCrud.Models;
using DotNetCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCrud.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Product> Products { get; set; } = new List<Product>();

        public IndexModel(ApplicationDbContext context) {
            this.context = context;
        }
        public void OnGet()
        {

            Products = context.Products.OrderByDescending(p=> p.Id).ToList();
        }
    }
}
