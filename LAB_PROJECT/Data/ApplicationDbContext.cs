using LAB_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LAB_PROJECT.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
