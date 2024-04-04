namespace ShoppingCart.Models;

public class Item {

    public int Id {get; }
    public int ProductCatalogueId {get; set;}
    public string ProductName {get; set;}
    public string Description {get; set;}
    public int Price {get; set;}

    public int CartId { get; }
    public Cart? Cart { get; set; } = null;

    public Item (int id, int ProductCatalogueId, string ProductName, string Description, int Price) {

	this.Id = id;
	this.ProductCatalogueId = ProductCatalogueId;
	this.ProductName = ProductName;
	this.Description = Description;
	this.Price = Price;

    }

	public virtual bool Equals(Item? obj) =>
	    obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);

	public override int GetHashCode() =>
	    this.ProductCatalogueId.GetHashCode();

}
