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
    /// Add one item to a user's shopping cart
    /// <param name="userId">ID of the user</param>
    /// </summary>
    /// <returns>
    /// The JSON representation of the created Cart object
    /// </returns>
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId)
    {

	// DRAFT
	Item shoppingCartItem = new Item("testName", 25);

	// Get the user's ShoppingCart
	var userCart = _context.Carts
	    .ToList<Cart>()
	    .SingleOrDefault(cart => cart.UserId == userId);

	String operationStatus;

	// Check if set is not empty
	if(userCart == null)
	{
	    logger.Debug("ShoppingCartController.AddItem", $"Cart for userId {userId} is empty");
	    operationStatus = "create";
	    // Create a new cart
	    logger.Debug("ShoppingCartController.AddItem", "Creating the Cart ...");
	    userCart = new Cart(){ UserId = userId };
	    _context.Add(userCart);
	}
	else
	{
	    operationStatus = "update";
	    logger.Debug("ShoppingCartController.AddItem", $"Cart for userId {userId} exists");
	    logger.Debug("ShoppingCartController.AddItem", "Updating the shopping cart ...");
	}

	// // TODO: check if item exists in the database or not
	// logger.Debug("ShoppingCartController.AddItem", "Creating the Item ...");
	// _context.Add(shoppingCartItem);
	// _context.SaveChanges();

	logger.Debug("ShoppingCartController.AddItem", "Adding the Item to the cart ...");
	userCart.Items.Add(shoppingCartItem);
	_context.SaveChanges();
	logger.Debug("ShoppingCartController.AddItem", "Saving done.");

	// TODO: check if the user exist

	Dictionary<string, object> res = new Dictionary<string, object>();
	res.Add("items", userCart.Serialize());

	// Set status: Update or Create
	res.Add("operation", operationStatus);

	return new JsonResult(res);
    }


    /// <summary>
    /// Get all items
    /// </summary>
    /// <returns>
    /// A JSON containing a list of Item
    /// </returns>
    [HttpGet("items")]
    public ActionResult GetItems()
    {
	const String unity = "ShoppingCartController.GetItems";
	IEnumerable<Item> items;
	Dictionary<String, object> res = new Dictionary<string, object>();
	logger.Debug(unity, "Getting all Items objects ...");
	items = _context.Items;
	res.Add("items", items);
	return new JsonResult(res);
    }

    /// <summary>
    /// Get all carts of all users.
    /// </summary>
    /// <returns>
    /// A JSON containing a list of Cart.
    /// </returns>
    [HttpGet("carts")]
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

}
