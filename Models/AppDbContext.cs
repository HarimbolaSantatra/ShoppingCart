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

	modelBuilder.Entity<Cart>()
	    .HasMany(cart => cart.Items)
	    .WithOne(item => item.Cart)
	    .HasForeignKey(item => item.CartId)
	    .IsRequired();

	modelBuilder.Entity<Item>(entity =>
		{
		entity.HasKey(e => e.Id);
		entity.Property(e => e.ProductCatalogueId).IsRequired();
		entity.Property(e => e.ProductName).IsRequired();
		entity.Property(e => e.Description).IsRequired();
		entity.Property(e => e.Price).IsRequired();
		});

    }

    public DbSet<Cart> ShoppingCartObjects { get; set; }
    public DbSet<Item> ShoppingCartItems { get; set; }

}
