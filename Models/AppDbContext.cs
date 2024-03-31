namespace ShoppingCart.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
	var connectionString = "server=localhost;user=root;password=root;database=shopping_cart";
	var serverVersion = new MariaDbServerVersion(new Version(15, 1));
	optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    public DbSet<ShoppingCartObj> ShoppingCartObjects { get; set; }

}
