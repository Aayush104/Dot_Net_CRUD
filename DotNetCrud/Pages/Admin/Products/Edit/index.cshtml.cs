using DotNetCrud.Models;
using DotNetCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCrud.Pages.Admin.Products.Edit
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;


        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public Product Product { get; set; } = new Product();

        public string errorMessage = "";
        public string successMessage = "";
        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            var product = context.Products.Find(id);

            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

          
            ProductDto.Name = product.Name;
            ProductDto.Brand = product.Brand;
            ProductDto.Category = product.Category;
            ProductDto.Price = product.Price;
            ProductDto.Description = product.Description;

            Product = product;
          
            
            
        }

        public void OnPost(int? id)
        {

            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the requireed Datas";
                return;
            }


            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;

            }
            Console.Write("This is products", product);
            //Update the image File in database
            string newFilename = product.ImageFileName; 

            if (ProductDto.ImageFile != null)
            {
                newFilename = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFilename += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath = Path.Combine(environment.WebRootPath, "Image", newFilename);

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }

                // Check if the old image file exists before attempting to delete it
                string oldFilePath = Path.Combine(environment.WebRootPath, "Image", product.ImageFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Update the product in the database
            product.Name = ProductDto.Name;
            product.Brand = ProductDto.Brand;
            product.Category = ProductDto.Category;
            product.Price = ProductDto.Price;
            product.Description = ProductDto.Description ?? "";

            if (ProductDto.ImageFile != null)
            {
                product.ImageFileName = $"{Request.Scheme}://{Request.Host}/Image/{newFilename}";
            }
            else
            {
                product.ImageFileName = newFilename;
            }
            context.SaveChanges();


            Product = product;


            successMessage = " Product Updated Successfully";
            Response.Redirect("/Admin/Products/Index"); ;


        }

    }
}
