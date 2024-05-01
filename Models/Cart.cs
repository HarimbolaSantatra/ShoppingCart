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

    // reference to child for one-to-many relationship
    public List<Item> Items { get; } = new List<Item>();

    // for one-to-one relationship
    public int UserId { get; set; }
    public User User { get; set; } = null;

    private MyLogger logger = new MyLogger("debug");

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
	res.Add("userId", this.UserId);

	ICollection<Dictionary<String, String>> arrayDict = new List<Dictionary<string, string>>();
	Dictionary<String, String> dict;
	foreach (var item in this.Items)
	{
	    dict = new Dictionary<String, String>();
	    dict.Add("Id", item.Id.ToString());
	    dict.Add("ProductCatalogueId", item.ProductCatalogueId.ToString());
	    dict.Add("ProductName", item.ProductName);
	    dict.Add("Description", item.Description);
	    dict.Add("Price", item.Price.ToString());
	    arrayDict.Add(dict);
	}
	res.Add("items", arrayDict);
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
