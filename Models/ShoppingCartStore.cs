namespace ShoppingCart.Models;
public class ShoppingCartStore : IShoppingCartStore
{
    private static readonly Dictionary<int, ShoppingCart>
	Database = new Dictionary<int, ShoppingCart>();

    // Use an in-memory dictionary instead of a database for now.
    public ShoppingCart Get(int userId) =>
	Database.ContainsKey(userId) ? Database[userId] : new ShoppingCart(userId);

    public void Save(ShoppingCart shoppingCart) =>
	Database[shoppingCart.UserId] = shoppingCart;

}