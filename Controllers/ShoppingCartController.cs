namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;
using ShoppingCart.Utils;

using Microsoft.EntityFrameworkCore;
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
	var res = new Dictionary<string, object>();
	var carts = new List<Dictionary<string, object>>();
	using (var context = new AppDbContext())
	{
	    logger.Debug("Querying context ...");
	    // Check if result is empty
	    List<Cart> cartList = context.ShoppingCartObjects.ToList<Cart>();
	    Cart userCart;
	    if (! Cart.IsEmpty(cartList))
	    {
		userCart = context.ShoppingCartObjects.First();
	    }
	    else
	    { 
		userCart = new Cart(){ UserId = userId };
		logger.Debug("userCart is empty");
	    }
	    carts.Add(userCart.Serialize(true));
	}
	res.Add("carts", carts);
	return new JsonResult(res);
    }


    // Add one item to a user shopping cart
    // TODO: FIX AddItem method of Controller: System.Text.Json.JsonException: A possible object cycle was detected
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId, [FromBody] Item shoppingCartItem)
    {
	using (var context = new AppDbContext())
	{

	    // Get the user's ShoppingCart
	    IEnumerable<Cart> userCarts = context.ShoppingCartObjects
		.ToList<Cart>()
		.Where(cart => cart.UserId == userId);

	    // One instance of userCart
	    Cart userCart;

	    // Check if set is not empty
	    if(! Cart.IsEmpty(userCarts))
	    {
		// TODO: take each cart possessed by the user
		// Only take the first
		userCart = context.ShoppingCartObjects.First();
	    }
	    else
	    {
		userCart = new Cart(){ UserId = userId };
		logger.Debug("userCart is empty");
	    }

	    // Update the ShoppingCart
	    logger.Debug("Updating the shopping cart ...");
	    userCart.AddItem(shoppingCartItem);

	    // Save the change in the database
	    logger.Debug("Saving ...");
	    context.ShoppingCartObjects.Add(userCart);
	    context.SaveChanges();

	    // Return the created object
	    Dictionary<string, object> res = userCart.Serialize();
	    return new JsonResult(res);
	}
    }


}
