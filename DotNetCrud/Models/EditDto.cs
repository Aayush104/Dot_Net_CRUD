using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DotNetCrud.Models
{
    public class EditDto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Brand { get; set; }

        [Required, MaxLength(100)]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? ImageFile { get; set; }

    }
}
