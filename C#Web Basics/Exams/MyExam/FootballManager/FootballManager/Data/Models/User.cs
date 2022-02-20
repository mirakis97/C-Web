using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    public class User
    {
        public User()
        {
            this.UserPlayers = new HashSet<UserPlayer>();
        }
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60,MinimumLength = 10)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<UserPlayer> UserPlayers { get; set; }
    }
}