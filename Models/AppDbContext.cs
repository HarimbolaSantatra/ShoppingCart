namespace ShoppingCart.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<ShoppingCartObj> ShoppingCartObjects { get; set; }

}
