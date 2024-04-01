namespace ShoppingCart.Models;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
	optionsBuilder.UseMySQL("server=localhost;database=shopping_cart;user=root;password=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	base.OnModelCreating(modelBuilder);

	// TODO: many-to-many relationship: https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
	modelBuilder.Entity<ShoppingCartObj>()
	    .HasMany(e => e.Items)
	    .WithOne(e => e.)

	modelBuilder.Entity<ShoppingCartItem>(entity =>
		{
		entity.HasKey(e => e.Id);
		entity.Property(e => e.ProductCatalogueId).IsRequired();
		entity.Property(e => e.ProductName).IsRequired();
		entity.Property(e => e.Description).IsRequired();
		entity.Property(e => e.Price).IsRequired();
		});

    }

    public DbSet<ShoppingCartObj> ShoppingCartObjects { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

}
