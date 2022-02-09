using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Data.Models
{
    public class Issue
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MinLength(5)]
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsFixed { get; set; }
        [Required]
        public string CarId { get; set; }
        public Car Car { get; set; }
    }
}