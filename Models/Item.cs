namespace ShoppingCart.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Item")]
public class Item {

    [Key]
    public int Id { get; set; }

    public int ProductCatalogueId { get; set; }
    public string ProductName { get; set; }
    public string? Description { get; set; } = String.Empty;
    public int Price { get; set; }

    public List<Cart> Carts { get; set; } = new List<Cart>();

    public Item(string productName, int price)
    {
	this.ProductName = productName;
	this.Price = price;
    }


    public virtual bool Equals(Item? obj) =>
	obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);

    public override int GetHashCode() =>
	this.ProductCatalogueId.GetHashCode();

}
