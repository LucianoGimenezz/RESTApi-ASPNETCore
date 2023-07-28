using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class TodoContext: DbContext, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todo");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}