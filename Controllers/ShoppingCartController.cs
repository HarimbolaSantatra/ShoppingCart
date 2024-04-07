namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;
using ShoppingCart.Utils;

using Microsoft.EntityFrameworkCore;

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
	    DbSet<Cart> set = context.ShoppingCartObjects;
	    bool isEmpty = false;
	    Cart userCart = new Cart(){ Id = userId};
	    if ( set != null && set.Any() )
	    {
		userCart = context.ShoppingCartObjects.First();
	    }
	    else
	    {
		logger.Debug("userCart is empty");
		isEmpty = true;
	    }
	    carts.Add(userCart.Serialize(isEmpty));
	}
	res.Add("carts", carts);
	return new JsonResult(res);
    }


    // Add one item to a user shopping cart
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId, [FromBody] Item shoppingCartItem)
    {
	using (var context = new AppDbContext())
	{

	    // Get the user's ShoppingCart
	    Cart shoppingCart = context.ShoppingCartObjects
		.Where(cart => cart.UserId == userId)
		.Single();

	    // Update the ShoppingCart
	    logger.Debug("Updating the shopping cart ...");
	    shoppingCart.AddItem(shoppingCartItem);

	    // Save the change in the database
	    logger.Debug("Saving ...");
	    context.ShoppingCartObjects.Add(shoppingCart);
	    context.SaveChanges();

	    // Return the created object
	    Dictionary<string, object> res = shoppingCart.Serialize();
	    return new JsonResult(res);
	}
    }


}
