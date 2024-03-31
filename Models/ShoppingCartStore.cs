namespace ShoppingCart.Models;

using Microsoft.AspNetCore.Mvc;

public class ShoppingCartStore : IShoppingCartStore
{

    private Logger logger;
    // Database: <userId, ShoppingCart>
    private Dictionary<int, ShoppingCart> Database = new Dictionary<int, ShoppingCart>();

    public ShoppingCartStore()
    {

	// Logger
	this.logger = new Logger("debug");

	// Populate with default data for userId 1
	ShoppingCart s1 = new ShoppingCart(1);
	// Add default item
	logger.Debug("Populating initial data ...");
	ShoppingCartItem defaultItem = new ShoppingCartItem(1, "Polo", "default", 9);
	s1.AddItem(defaultItem);
	Database.Add(1, s1);
    }

    // Get what's inside a user's cart
    public ShoppingCart Get(int userId)
    {
	if (Database.ContainsKey(userId))
	    return Database[userId];
	else
	    return new ShoppingCart(userId);
    }

    // Check if a user's cart is empty
    public bool isEmpty(int userId) => Database.ContainsKey(userId);

    // Save the shoppingCart into the database
    public void Save(ShoppingCart shoppingCart)
    {
	Database[shoppingCart.UserId] = shoppingCart;
    }

}
