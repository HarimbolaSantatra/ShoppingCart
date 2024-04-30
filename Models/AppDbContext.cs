namespace ShoppingCart.Models;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<Cart> Carts { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
	optionsBuilder.UseMySQL("server=localhost;database=shopping_cart;user=root;password=root");
    }

}
