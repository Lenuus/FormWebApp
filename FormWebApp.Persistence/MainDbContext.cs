using FormApp.Domain;
using FormWebApp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FormWebApp.Persistence
{
    public class MainDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public DbSet<Field> Fields { get; set; }
        public DbSet<Form> Forms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(p => p.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<UserRole>().HasOne(p => p.User).WithMany(p => p.Roles).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRole>().HasOne(p => p.Role).WithMany(p => p.Users).HasForeignKey(p => p.RoleId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(f => f.Forms).WithOne(f => f.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasMany(f => f.Fields).WithOne(f => f.Form).HasForeignKey(f => f.FormId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Field>().Property(p => p.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Form>().Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
