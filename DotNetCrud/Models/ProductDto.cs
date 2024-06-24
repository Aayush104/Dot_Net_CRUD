﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DotNetCrud.Models
{
    public class ProductDto
    {
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";

        [MaxLength(100)]
        public string Category { get; set; } = "";

        [Precision(16, 2)]
        public decimal Price { get; set; }

        public string Description { get; set; } = "";

        public IFormFile? ImageFile { get; set; }
    }
}
