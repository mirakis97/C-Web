﻿namespace Git.Data
{
    using Git.Data.Models;
    using Git.ViewModels.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Git;Integrated Security=true;");
            }
        }

        internal List<AllRepositoryListModel> Where(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}