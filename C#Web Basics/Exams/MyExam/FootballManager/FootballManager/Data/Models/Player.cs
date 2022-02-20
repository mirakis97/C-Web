﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    public class Player
    {
        public Player()
        {
            this.UserPlayers = new HashSet<UserPlayer>();
        }
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(80,MinimumLength = 5)]
        public string FullName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Position { get; set; }
        [Required]
        [Range(0,10)]
        public byte Speed { get; set; }
        [Required]
        [Range(0, 10)]
        public byte Endurance { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public ICollection<UserPlayer> UserPlayers { get; set; }
    }
}