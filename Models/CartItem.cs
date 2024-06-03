namespace ShoppingCart.Models;

/// <summary>
/// Represent a Cart and a list of item that is inside that Cart
/// Usage: for controller that takes both a Cart and an Item as an arguments
/// </summary>
public class CartItem {

    public int cartId { get; set; }
    public IEnumerable<int> itemsId { get; set; }

}
