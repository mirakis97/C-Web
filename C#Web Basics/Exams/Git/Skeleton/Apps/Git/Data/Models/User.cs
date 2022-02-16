using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            this.Commits = new HashSet<Commit>();
            this.Repositories = new HashSet<Repository>();
        }
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Repository> Repositories { get; set; }
        public ICollection<Commit> Commits { get; set; }
    }
}