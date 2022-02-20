using System;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.ViewModels.Players
{
    public class PlayerViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "{0} is required , and must be between {2} and {1} characters")]
        public string FullName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} is required , and must be between {2} and {1} characters")]
        public string Position { get; set; }
        
        [Required]
        public string Speed { get; set; }
        [Required]
        public string Endurance { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 0, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Description { get; set; }
    }
}
