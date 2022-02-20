using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class UserPlayer
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }
    }
}