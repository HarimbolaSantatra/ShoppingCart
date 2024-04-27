namespace ShoppingCart.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("User")]
public class User {

    [Key]
    public int Id { get; set; }

    public string Username { get; set; }

    // for one-to-one relationship
    public Cart? cart { get; set; }

    public User(string username)
    {
	this.Username = username;
    }

}
