using DotNetCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCrud.Pages.Admin.Products.Delete
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public IndexModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {

            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = context.Products.Find(id);
            Console.WriteLine("uvj {0}",product.ImageFileName);
            if (product == null) {
                Response.Redirect("/Admin/Products/Index");
                return;
            }


            String ImageFullPath = product.ImageFileName;
            if (System.IO.File.Exists(ImageFullPath))
            {
                System.IO.File.Delete(ImageFullPath);
            }
            

            context.Products.Remove(product);
            context.SaveChanges();
             Response.Redirect("/Admin/Products/Index");

        }
    }
}
