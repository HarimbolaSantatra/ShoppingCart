namespace ShoppingCart.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// A item inside a cart
/// </summary>
[Table("Item")]
public class Item {

    [Key]
    public int Id { get; set; }

    public int ProductCatalogueId { get; set; }
    public string ProductName { get; set; }
    public string? Description { get; set; } = String.Empty;
    public int Price { get; set; }

    // Reference to parent for one-to-many relationship
    public int CartId { get; set; } // Foreign key
    public Cart Cart { get; set; } = null; // Reference to the parent

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
