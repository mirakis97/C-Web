using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data.Models
{
    public class Commit
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public User Creator { get; set; }
        public string RepositoryId { get; set; }
        [ForeignKey(nameof(RepositoryId))]
        public Repository Repository { get; set; }
    }
}