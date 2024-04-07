namespace ShoppingCart.Models;

using System.ComponentModel.DataAnnotations;

public class Item {

    [Key]
    public int Id { get; set; }

    public int ProductCatalogueId { get; set;}
    public string ProductName { get; set;}
    public string Description { get; set;}
    public int Price { get; set;}

    public int CartId { get; }
    public Cart? Cart { get; set; } = null;

    public virtual bool Equals(Item? obj) =>
	obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);

    public override int GetHashCode() =>
	this.ProductCatalogueId.GetHashCode();

}
