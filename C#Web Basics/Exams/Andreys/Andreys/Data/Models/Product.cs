using Andreys.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Andreys.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
        [Required]
        public ProductGender Gender { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}