namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;
using ShoppingCart.Utils;

using System.Linq;

[ApiController]
[Route("/shoppingcart")]
public class ShoppingCartController
{

    private MyLogger logger = new MyLogger("debug");

    public ShoppingCartController()
    { }


    // Declares the endpoint for handling requests to /shoppingcart/{userid}
    [HttpGet("")]
    public ActionResult Index()
    {
	var res = new Dictionary<String, String>();
	res.Add("status", "ShoppingCart microservice is working!");
	return new JsonResult(res);
    }


    // Get a user's cart
    [HttpGet("{userId:int}")]
    public ActionResult GetUserCart(int userId)
    {
	const String unity = "ShoppingCartController.GetUserCart";
	var res = new Dictionary<string, object>();
	using (var context = new AppDbContext())
	{
	    logger.Debug(unity, "Querying context ...");
	    // Get the user's ShoppingCart
	    var userCart = context.ShoppingCartObjects
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
		    userCart = context.ShoppingCartObjects.Single(cart => cart.UserId == userId);
		    res.Add("status", "exist");
		    res.Add("cart", userCart.Serialize());
		}
		catch(InvalidOperationException)
		{
		    logger.Error(unity, $"userCart of userId {userId} contains more than one element");
		    res.Add("status", $"Error: userCart of userId {userId} contains more than one element");
		}
	    }
	}
	return new JsonResult(res);
    }


    // Add one item to a user shopping cart
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId, [FromBody] Item shoppingCartItem)
    {
	using (var context = new AppDbContext())
	{

	    // Get the user's ShoppingCart
	    var userCart = context.ShoppingCartObjects
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
	    context.Add(userCart);
	    context.SaveChanges();

	    // Return the created object
	    logger.Debug("ShoppingCartController.AddItem", "Saved to the database. Serializing ...");

	    // TODO: FIX AddItem method of Controller: System.Text.Json.JsonException: A possible object cycle was detected
	    // Path: $.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items.Cart.Items
	    Dictionary<string, object> res = userCart.Serialize();

	    // Set status: Update or Create
	    logger.Debug("Serialization done.");
	    res.Add("operation", operationStatus);

	    return new JsonResult(res);
	}
    }


}
