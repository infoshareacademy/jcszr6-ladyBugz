﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MagicDish.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicDish.Persistance.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Fridge> Fridges { get; set; }
		public DbSet<FridgeProduct> FridgeProducts { get; set; }
		public DbSet<Product> AvailableProducts { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Unit> Units { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            CreateProductCategories(modelBuilder);
			CreateUnit(modelBuilder);
			CreateProduct(modelBuilder);
		}

        private void CreateProductCategories(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductCategory>()
				.ToTable("ProductCategories");
			modelBuilder.Entity<ProductCategory>()
				.Property(s => s.CategoryName)
				.IsRequired(true);
			modelBuilder.Entity<ProductCategory>()
				.HasData(
					new ProductCategory
					{
						Id = 1,
						CategoryName = "Vegetable"
					},
					new ProductCategory
					{
						Id = 2,
						CategoryName = "Fruit"
					},
					new ProductCategory
					{
						Id = 3,
						CategoryName = "Dairy"
					},
					new ProductCategory
					{
						Id = 4,
						CategoryName = "Starch"
					},
					new ProductCategory
					{
						Id = 5,
						CategoryName = "Fat"
					},
					new ProductCategory
					{
						Id = 6,
						CategoryName = "Spices"
					}
				);
		}

		private void CreateProduct(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.ToTable("AvailableProducts");

			modelBuilder.Entity<Product>()
				.Property(s => s.Name)
				.IsRequired(true);

			modelBuilder.Entity<Product>()
				.Property(s => s.UnitId)
				.IsRequired(true);

			modelBuilder.Entity<Product>()
				.Property(s => s.ProductCategoryId)
				.IsRequired(true);

			modelBuilder.Entity<Product>()
				.HasData(
					new Product
					{
						Id = 1,
						Name = "Tomato",
						UnitId = 1,
						ProductCategoryId = 1
					},
					new Product
					{
						Id = 2,
						Name = "Potato",
						UnitId = 1,
						ProductCategoryId = 1
					},
					new Product
					{
						Id = 3,
						Name = "Carrot",
						UnitId = 1,
						ProductCategoryId = 1
					},
					new Product
					{
						Id = 4,
						Name = "Apple",
						UnitId = 1,
						ProductCategoryId = 2
					},
					new Product
					{
						Id = 5,
						Name = "Lemon",
						UnitId = 1,
						ProductCategoryId = 2
					},
					new Product
					{
						Id = 6,
						Name = "Cheese",
						UnitId = 1,
						ProductCategoryId = 3
					},
					new Product
					{
						Id = 7,
						Name = "Milk",
						UnitId = 2,
						ProductCategoryId = 3
					},
					new Product
					{
						Id = 8,
						Name = "Flour",
						UnitId = 1,
						ProductCategoryId = 4
					},
					new Product
					{
						Id = 9,
						Name = "Pasta",
						UnitId = 1,
						ProductCategoryId = 4
					},
					new Product
					{
						Id = 10,
						Name = "Olive oil",
						UnitId = 2,
						ProductCategoryId = 5
					},
					new Product
					{
						Id = 11,
						Name = "Butter",
						UnitId = 1,
						ProductCategoryId = 5
					},
					new Product
					{
						Id = 12,
						Name = "Salt",
						UnitId = 1,
						ProductCategoryId = 6
					},
					new Product
					{
						Id = 13,
						Name = "Pepper",
						UnitId = 1,
						ProductCategoryId = 6
					}
				);
		}

		private void CreateUnit(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Unit>()
				.ToTable("Units");
			modelBuilder.Entity<Unit>()
				.Property(s => s.UnitName)
				.IsRequired(true);
			modelBuilder.Entity<Unit>()
				.HasData(
					new Unit
					{
						Id = 1,
						UnitName = "Grams"
					},
					new Unit
					{
						Id = 2,
						UnitName = "Mililiters"
					}
				);
		}
	}
}