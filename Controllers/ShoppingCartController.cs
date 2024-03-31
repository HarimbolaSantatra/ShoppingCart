namespace ShoppingCart.Controllers;

using Microsoft.AspNetCore.Mvc;

using ShoppingCart.Models;

    public ShoppingCartController()
    {
    }


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
	ShoppingCartObj userCart = 
	return new JsonResult(userCart.Serialize(userCart.Items.Count == 0));
    }


    // Add one item to a user shopping cart
    [HttpPost("{userId:int}/item")]
    public ActionResult AddItem(int userId, [FromBody] ShoppingCartItem shoppingCartItem)
    {

	// Get the user's ShoppingCart
	ShoppingCartObj shoppingCart = 
	// Update the ShoppingCart
	shoppingCart.AddItem(shoppingCartItem);

	Dictionary<string, object> res = shoppingCart.Serialize();
	return new JsonResult(res);
    }


    // Add multiple items to a user shopping cart
    [HttpPost("{userId:int}/items")]
    public void AddItems(int userId, IEnumerable<ShoppingCartItem> shoppingCartItems)
    {
	// Get the user's ShoppingCart
	ShoppingCartObj shoppingCart = 
	// Update the ShoppingCart
	shoppingCart.AddItems(shoppingCartItems);
    }


}
