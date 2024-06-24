using DotNetCrud.Models;
using DotNetCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetCrud.Pages.Admin.Products.Create
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]

        public ProductDto ProductDto { get; set; } = new ProductDto();


        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
       
        public void OnGet()
        {


            

    }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {

            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The image file is require");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please Provide all the required fields";
                return;

            }

            //save the image File

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/Image/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                

                ProductDto.ImageFile.CopyTo(stream);
            }
            

            //save the products 
            Product product = new Product()
            {
                Name = ProductDto.Name,
                    Brand = ProductDto.Brand,
                    Category = ProductDto.Category,
                    Price = ProductDto.Price,
                    Description = ProductDto.Description ?? "",
                    ImageFileName = $"{Request.Scheme}://{Request.Host}/Image/{newFileName}"


            };

            context.Products.Add(product);
            context.SaveChanges();


            ProductDto.Name = ""; 
            ProductDto.Brand = "";  
            ProductDto.Category = "";
            ProductDto.Price = 0 ;
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();
            successMessage = "Product Created Sucessfully";

            Response.Redirect("/Admin/Products/Index");


        }
    }
}
