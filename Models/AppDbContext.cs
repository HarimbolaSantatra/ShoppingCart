namespace ShoppingCart.Models;

using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<Cart> ShoppingCartObjects { get; set; }
    public DbSet<Item> ShoppingCartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
	optionsBuilder.UseMySQL("server=localhost;database=shopping_cart;user=root;password=root");
    }

}
