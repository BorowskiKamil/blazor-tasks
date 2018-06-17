using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlazorTasks.Server.Models.Entities;

namespace BlazorTasks.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions opt) 
            : base (opt) { }

        public DbSet<TodoTaskEntity> Tasks { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

    }
}
