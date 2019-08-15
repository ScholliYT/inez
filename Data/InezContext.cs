using INEZ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace INEZ.Data
{
    public class InezContext : DbContext
    {
        public InezContext(DbContextOptions<InezContext> options) : base(options)
        { 
        }

        public DbSet<Item> Items { get; set; }
    }
}

