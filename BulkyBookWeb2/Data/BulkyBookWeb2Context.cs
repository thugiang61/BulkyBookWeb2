using BulkyBookWeb2.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb2.Data
{
    public class BulkyBookWeb2Context : DbContext
    {
        public BulkyBookWeb2Context(DbContextOptions<BulkyBookWeb2Context> options) : base(options)
        {
        }

#nullable disable
        public DbSet<Book> Book { get; set; }
    }
}
