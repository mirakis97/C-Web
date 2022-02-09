using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Issues = new HashSet<Issue>();
        }
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Model { get; set; }
        public int Year { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        [RegularExpression(@"[A-Z]{2}[0-9]{4}[A-Z]{2}")]
        public string PlateNumber { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }
}