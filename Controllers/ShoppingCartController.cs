namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;
using ShoppingCart.Utils;

using System.Linq;

[ApiController]
[Route("/shoppingcart")]
public class ShoppingCartController
{

    private readonly AppDbContext _context;
    private MyLogger logger = new MyLogger("debug");

    public ShoppingCartController(AppDbContext context)
    {
	_context = context;
    }


    /// <summary>
    /// Declares the endpoint for handling requests to /shoppingcart/
    /// </summary>
    /// <returns>
    /// Test JSON status
    /// </returns>
    [HttpGet("")]
    public ActionResult Index()
    {
	var res = new Dictionary<String, String>();
	res.Add("status", "ShoppingCart microservice is working!");
	return new JsonResult(res);
    }


    /// <summary>
    /// Get a user's cart
    /// <param name="userId">ID of the user</param>
    /// </summary>
    /// <returns>
    /// The Cart object
    /// </returns>
    [HttpGet("{userId:int}")]
    public ActionResult GetUserCart(int userId)
    {
	const String unity = "ShoppingCartController.GetUserCart";
	var res = new Dictionary<string, object>();
	logger.Debug(unity, "==== GetUserCart begin ====");

	var userCart = _context.Carts
	    .SingleOrDefault(cart => cart.UserId == userId);
	// Check if result is empty
	if (userCart == null)
	{
	    logger.Debug(unity, $"Cart for user {userId} is empty");
	    res.Add("status", "empty");
	}
	else
	{
	    logger.Debug(unity, $"Cart for user {userId} exists");
	    res.Add("status", "exist");
	    res.Add("cart", _context.Carts);
	}
	return new JsonResult(res);
    }


    /// <summary>
    /// Add a list of Item to the cart
    /// </summary>
    /// <param name="cartItem">Represent a cart and an item</param>
    /// <returns>
    /// The JSON representation of the created Item object
    /// </returns>
    [HttpPost("item")]
    public ActionResult AddItems(CartItem cartItem)
    {

	//TODO: make the controller async

	Dictionary<string, object> res = new Dictionary<string, object>();

	// Get the cart
	var cart = _context.Carts.ToList().SingleOrDefault(cart => cart.Id == cartItem.cartId);
	if (cart == null)
	{
	    logger.Error("ShoppingCartController.AddItems", "Cart doesn't exist");
	    res.Add("operation", "inexistant_cart");
	    return new JsonResult(res);
	}

	// Get the item
	IEnumerable<Item> items = _context.Items.ToList().Where(item => cartItem.itemsId.Contains(item.Id));
	if (items == null)
	{
	    logger.Error("ShoppingCartController.AddItems", "Item doesn't exist");
	    res.Add("operation", "inexistant_item");
	    return new JsonResult(res);
	}

	logger.Debug("ShoppingCartController.AddItem", "Adding the Item to the cart ...");

	foreach (Item item in items)
	{
	    cart.Items.Add(item);
	}
	_context.SaveChanges();
	logger.Debug("ShoppingCartController.AddItem", "Update done.");

	// TODO: check if the user exist

	res.Add("items", cart.Serialize());

	// Set status: Update or Create
	res.Add("operation", "updated");

	return new JsonResult(res);
    }


    /// <summary>
    /// Get all items inside a cart
    /// </summary>
    /// <returns>
    /// A JSON containing a list of Item
    /// </returns>
    [HttpGet("items")]
    public ActionResult GetItems(int cartId)
    {
	const String unity = "ShoppingCartController.GetItems";
	IEnumerable<Item> items;
	Dictionary<String, object> res = new Dictionary<string, object>();
	logger.Debug(unity, "Getting all Items objects ...");
	items = _context.Items.ToList<Item>().Where(item => item.CartId == cartId);
	res.Add("items", items);
	return new JsonResult(res);
    }


    /// <summary>
    /// Get all carts
    /// </summary>
    /// <returns>
    /// A JSON containing a list of Cart.
    /// </returns>
    [HttpGet("cart")]
    public ActionResult GetCarts()
    {
	const String unity = "ShoppingCartController.GetCarts";
	IEnumerable<Cart> carts;
	Dictionary<String, object> res = new Dictionary<string, object>();
	logger.Debug(unity, "Getting all Cart objects ...");
	carts = _context.Carts;
	res.Add("carts", carts);
	return new JsonResult(res);
    }


    /// <summary>
    /// Add a cart to the database
    /// </summary>
    /// <returns>
    /// The JSON representation of the created Cart object
    /// </returns>
    [HttpPost("cart")]
    public ActionResult AddCart (Cart cart)
    {

	Dictionary<string, object> res = new Dictionary<string, object>();

	// Get the cart
	var carts = _context.Carts;
	if (carts == null)
	{
	    logger.Error("ShoppingCartController.AddCart", "Cart is null");
	    res.Add("operation", "inexistant_cart");
	    return new JsonResult(res);
	}

	logger.Debug("ShoppingCartController.AddCart", "Adding the Cart to the database ...");

	carts.Add(cart);
	_context.SaveChanges();
	logger.Debug("ShoppingCartController.AddCart", "Update done.");

	res.Add("cart", cart.Serialize());

	// Set status: Update or Create
	res.Add("operation", "updated");

	return new JsonResult(res);
    }
}
