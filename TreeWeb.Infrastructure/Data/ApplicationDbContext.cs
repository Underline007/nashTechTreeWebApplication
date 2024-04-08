using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Core.Entities;

namespace TreeWeb.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // product and product related  
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // payment and cart
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> orderStatuses { get; set; }

        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Indoor plants" },
                new Category { CategoryId = 2, Name = "Shrubs Bushes" },
                new Category { CategoryId = 3, Name = "Succulents Cacti " }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", Price = 10.99m, Quantity = 5, CategoryId = 1, SupplierId = 1 },
                new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", Price = 15.99m, Quantity = 10, CategoryId = 2, SupplierId = 2 },
                new Product { ProductId = 3, Name = "Product 3", Description = "Description 3", Price = 20.99m, Quantity = 8, CategoryId = 3, SupplierId = 3 }
            );


            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, Name = "Supplier 1", Address = "Address 1", PhoneNumber = "123-456-7890" },
                new Supplier { SupplierId = 2, Name = "Supplier 2", Address = "Address 2", PhoneNumber = "456-789-0123" },
                new Supplier { SupplierId = 3, Name = "Supplier 3", Address = "Address 3", PhoneNumber = "789-012-3456" }
            );

            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    Name = "Admin",
                    Address = "Admin Address"
                },
                new AppUser
                {
                    Id = "2",
                    UserName = "supplier1@example.com",
                    NormalizedUserName = "SUPPLIER1@EXAMPLE.COM",
                    Email = "staff1@example.com",
                    NormalizedEmail = "STAFF1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    Name = "Supplier 1",
                    Address = "Supplier 1 Address"
                },
                new AppUser
                {
                    Id = "3",
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    Name = "User",
                    Address = "User Address"
                },
                new AppUser
                {
                    Id = "4",
                    UserName = "supplier2@example.com",
                    NormalizedUserName = "SUPPLIER2@EXAMPLE.COM",
                    Email = "staff2@example.com",
                    NormalizedEmail = "STAFF2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Abc@123"),
                    SecurityStamp = string.Empty,
                    Name = "Supplier 2",
                    Address = "Supplier 2 Address"
                }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "admin_role_id", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "user_role_id", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "staff_role_id", Name = "Staff", NormalizedName = "STAFF" }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "admin_role_id", UserId = "1" },
                new IdentityUserRole<string> { RoleId = "staff_role_id", UserId = "2" },
                new IdentityUserRole<string> { RoleId = "user_role_id", UserId = "3" },
                new IdentityUserRole<string> { RoleId = "staff_role_id", UserId = "4" }
            );

        }
    }
}
