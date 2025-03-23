using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.Model;

namespace ToDoList.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TaskResponseModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the TaskInfoResponseModel entity
            modelBuilder.Entity<TaskResponseModel>(entity =>
            {
                entity.HasKey(t => t.Id); // Set Id as the primary key
                entity.Property(t => t.Title).IsRequired().HasMaxLength(100); // Title is required and has a max length of 100
                entity.Property(t => t.Description).HasMaxLength(500); // Description has a max length of 500
                entity.Property(t => t.CreatedDate).HasDefaultValueSql("GETUTCDATE()"); // Set default value for CreatedDate
                entity.Property(t => t.UpdatedDate).IsRequired(false); // UpdatedDate is optional
            });
        }
    }
}
