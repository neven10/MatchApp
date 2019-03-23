using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MatchDbContext : DbContext
    {
        public MatchDbContext(DbContextOptions<MatchDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchTime> MatchTimes  { get; set; }
        public DbSet<Stakes>  Stakes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
