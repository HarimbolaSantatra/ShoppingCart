namespace ShoppingCart.Models;

using ShoppingCart.Utils;

using System.Collections.Generic;
using System.Linq;

public class Cart
{

    public int Id { get; }
    public int UserId { get; }
    private MyLogger logger = new MyLogger("debug");

    public ICollection<Item> Items { get; } = new List<Item>();


    // Constructor
    public Cart(int id, int userId)
    {
	this.Id = id;
	this.UserId = userId;
    }


    public Dictionary<String, object> Serialize(bool empty=false)
    {
	Dictionary<String, object> res = new Dictionary<String, object>();
	res.Add("userId", ( this.UserId ).ToString());
	if (empty || this.Items.Count == 0 )
	    res.Add("items", "[]");
	else
	{
	    Dictionary<String, String>[] arrayItem = {};
	    Dictionary<String, String> dict = new Dictionary<String, String>();
	    foreach (var shoppingCartItem in this.Items)
	    {
		dict.Add("ProductCatalogueId", shoppingCartItem.ProductCatalogueId.ToString());
		dict.Add("ProductName", shoppingCartItem.ProductName);
		dict.Add("Description", shoppingCartItem.Description);
		dict.Add("Price", shoppingCartItem.Price.ToString());
		arrayItem.Append(dict);
	    }
	    res.Add("items", Items);
	}
	return res;
    }


    public void AddItem(Item shoppingCartItem)
    {
	this.Items.Add(shoppingCartItem);

	// LOGGING: print the Hast Set: this.Items
	string logs = "";
	logs = "Element in the resulting set: ";
	logs += "{";
	foreach (var item in this.Items)
	{
	    logs += $"ProductCatalogueId: {item.ProductCatalogueId} - ";
	    logs += $"ProductName: {item.ProductName} - ";
	    logs += $"Description: {item.Description} - ";
	    logs += $"Price: {item.Price}";
	}
	logs += " }";
	logger.Debug(logs);

    }

    public void AddItems(IEnumerable<Item> shoppingCartItems)
    {
	foreach (var item in shoppingCartItems)
	    this.Items.Add(item);
    }

    /// <summary>
    /// Method RemoveItems removes all the items in the collection
    /// </summary>
    public void RemoveItems(ICollection<Item> items)
    {
	foreach (var item in items)
	    this.Items.Remove(item);
    }
}
