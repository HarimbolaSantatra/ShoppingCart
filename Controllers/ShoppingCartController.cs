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
	logger.Debug(unity, "Querying _context ...");
	// Get the user's ShoppingCart
	var userCart = _context.ShoppingCartObjects
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
	    try {
		userCart = _context.ShoppingCartObjects.Single(cart => cart.UserId == userId);
		res.Add("status", "exist");
		res.Add("cart", userCart.Serialize());
	    }
	    catch(InvalidOperationException)
	    {
		logger.Error(unity, $"userCart of userId {userId} contains more than one element");
		res.Add("status", $"Error: userCart of userId {userId} contains more than one element");
	    }
	}
	return new JsonResult(res);
    }


    
    /// <summary>
    /// Add one item to a user shopping cart
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
	var userCart = _context.ShoppingCartObjects
	    .ToList<Cart>()
	    .SingleOrDefault(cart => cart.UserId == userId);

	String operationStatus;

	// Check if set is not empty
	if(userCart == null)
	{
	    logger.Debug("ShoppingCartController.AddItem", $"Cart for userId {userId} is empty");
	    operationStatus = "create";
	    // Create a new cart
	    userCart = new Cart(){ UserId = userId };
	    logger.Debug("ShoppingCartController.AddItem", "Creating the shopping cart ...");
	}
	else
	{
	    operationStatus = "update";
	    logger.Debug("ShoppingCartController.AddItem", $"Cart for userId {userId} exists");
	    logger.Debug("ShoppingCartController.AddItem", "Updating the shopping cart ...");
	}

	// Update the ShoppingCart
	userCart.Items.Add(shoppingCartItem);

	// Save the change in the database
	logger.Debug("ShoppingCartController.AddItem", $"Saving cart --- Id: {userCart.Id}; UserId: {userCart.UserId}");
	_context.SaveChanges();

	// Return the created object
	logger.Debug("ShoppingCartController.AddItem", "Saved to the database. Serializing ...");

	Dictionary<string, object> res = userCart.Serialize();

	// Set status: Update or Create
	logger.Debug("Serialization done.");
	res.Add("operation", operationStatus);

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
	carts = _context.ShoppingCartObjects;
	res.Add("carts", carts);
	return new JsonResult(res);
    }

}
