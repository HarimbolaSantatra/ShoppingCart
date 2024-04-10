namespace ShoppingCart.Models;

using ShoppingCart.Utils;

using System.Collections.Generic;
using System.Linq;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Cart")]
public class Cart
{

    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    private MyLogger logger = new MyLogger("debug");

    public List<Item> Items { get; set; } = new List<Item>();


    /// <summary>
    /// Method checkIfEmpty check if a list of Cart is empty or not
    /// </summary>
    public static bool IsEmpty(IEnumerable<Cart> carts)
    {
	if (carts != null && carts.Any())
	{
	    return false;
	}
	return true;
    }


    public Dictionary<String, object> Serialize()
    {
	Dictionary<String, object> res = new Dictionary<String, object>();
	res.Add("userId", ( this.UserId ).ToString());

	// check if Cart doesn't contain any items
	if (this.Items == null || this.Items.Count == 0 )
	{
	    logger.Debug("Cart.Serialize", "this.Items doesn't contain any items.");
	    res.Add("items", "[]");
	    return res;
	}

	Dictionary<String, String>[] arrayItem = {};
	Dictionary<String, String> dict;
	foreach (var item in this.Items)
	{
	    dict = new Dictionary<String, String>();
	    dict.Add("Id", item.Id.ToString());
	    dict.Add("ProductCatalogueId", item.ProductCatalogueId.ToString());
	    dict.Add("ProductName", item.ProductName);
	    dict.Add("Description", item.Description);
	    dict.Add("Price", item.Price.ToString());
	    arrayItem.Append(dict);
	}
	res.Add("items", arrayItem);
	return res;
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
