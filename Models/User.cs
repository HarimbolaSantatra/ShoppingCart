namespace ShoppingCart.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("User")]
public class User {

    [Key]
    public int Id { get; set; }

    public string Username { get; set; }

    public User(string username)
    {
	this.Username = username;
    }

}
