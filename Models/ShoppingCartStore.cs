namespace ShoppingCart.Models;

using ShoppingCart.Utils;

public class ShoppingCartStore : IShoppingCartStore
{

    private MyLogger logger;
    // Database: <userId, ShoppingCart>
    private Dictionary<int, ShoppingCartObj> Database = new Dictionary<int, ShoppingCartObj>();

    public ShoppingCartStore()
    {

	// Logger
	this.logger = new MyLogger("debug");

	// Populate with default data for userId 1
	ShoppingCartObj s1 = new ShoppingCartObj(1);
	// Add default item
	logger.Debug("Populating initial data ...");
	ShoppingCartItem defaultItem = new ShoppingCartItem(1, "Polo", "default", 9);
	s1.AddItem(defaultItem);
	Database.Add(1, s1);
    }

    // Get what's inside a user's cart
    public ShoppingCartObj Get(int userId)
    {
	if (Database.ContainsKey(userId))
	    return Database[userId];
	else
	    return new ShoppingCartObj(userId);
    }

    // Check if a user's cart is empty
    public bool isEmpty(int userId) => Database.ContainsKey(userId);

    // Save the shoppingCart into the database
    public void Save(ShoppingCartObj shoppingCart)
    {
	Database[shoppingCart.UserId] = shoppingCart;
    }

}
