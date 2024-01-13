using ClearXchange.Server.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClearXchange.Server.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options)
            : base(options)
        {
 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        // Additional configuration, if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             //modelBuilder.Entity<Member>().ToTable("Members");
        }

        //public IEnumerable<EntityEntry> GetModifiedEntries()
        //{
        //    return ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
        //}
        public DbSet<Member>? Members { get; set; }
    }
}
