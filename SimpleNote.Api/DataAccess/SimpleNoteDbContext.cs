using Microsoft.EntityFrameworkCore;
using SimpleNote.Api.Models;

namespace SimpleNote.Api.DataAccess
{
    public class SimpleNoteDbContext : DbContext
    {
        public SimpleNoteDbContext(DbContextOptions<SimpleNoteDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
    }
}
