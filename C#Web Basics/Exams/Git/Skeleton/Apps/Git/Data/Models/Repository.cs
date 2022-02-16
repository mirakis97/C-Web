using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Git.Data.Models
{
    public class Repository
    {
        public Repository()
        {
            this.Commits = new HashSet<Commit>();
        }
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }
        public ICollection<Commit> Commits { get; set; }
    }
}