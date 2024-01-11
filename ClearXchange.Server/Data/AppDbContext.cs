using ClearXchange.Server.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClearXchange.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
 
        }

        public DbSet<User>? Users { get; set; }
    }
}
