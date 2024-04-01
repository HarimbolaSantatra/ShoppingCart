namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;


[ApiController]
[Route("/shoppingcart")]
public class ShoppingCartController
{

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
	    ShoppingCartObj userCart = context.ShoppingCartObjects.First();
	    carts.Add(userCart.Serialize(userCart.Items.Count == 0));
	}
	res.Add("carts", carts);
	return new JsonResult(res);
    }


    // Add one item to a user shopping cart
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId, [FromBody] ShoppingCartItem shoppingCartItem)
    {
	using (var context = new AppDbContext())
	{

	    // Get the user's ShoppingCart
	    ShoppingCartObj shoppingCart = context.ShoppingCartObjects
		.Where(cart => cart.UserId == userId)
		.Single();

	    // Update the ShoppingCart
	    shoppingCart.AddItem(shoppingCartItem);

	    // Save the change in the database
	    context.ShoppingCartObjects.Add(shoppingCart);
	    context.SaveChanges();

	    // Return the created object
	    Dictionary<string, object> res = shoppingCart.Serialize();
	    return new JsonResult(res);
	}
    }


}
