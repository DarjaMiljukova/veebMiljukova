using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace veebMiljukova.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Toode> Tooted { get; set; }
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Cart> Ostukorvid { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}