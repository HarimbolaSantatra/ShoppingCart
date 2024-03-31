namespace ShoppingCart.Models;

public interface IShoppingCartStore
{
    ShoppingCartObj Get(int userId);
    void Save(ShoppingCartObj shoppingCart);
}
