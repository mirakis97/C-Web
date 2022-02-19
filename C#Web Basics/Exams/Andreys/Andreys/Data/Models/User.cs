using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Andreys.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(10,MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}