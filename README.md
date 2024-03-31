# gammerlgaard-shopping-cart
ShoppingCart services for the Shopping Cart microservices, based on [Gammerlgaard's book][1].
Master repo is [here](https://github.com/HarimbolaSantatra/gammerlgaard-shopping-cart).

## ABOUT THE PROJECT
### Branch
| Branch | Description |
| --- | --- |
| main | My implementation |
| book/dummyInterface | Add a a dummy implementation of the IShoppingCartStore interface |
| dev/removeItem | **Implementaton**: remove item from a shoppingCart |

> [!IMPORTANT]
> .NET Core do not use `startup.cs` anymore, which is still used in the book; instead, I use `Program.cs` on the *master* branch.

### Database
**Name**: *shopping_cart*

## Resources:
- [ Gammerlgaard's book ][1]
- [Containerize a .NET app](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=linux&pivots=dotnet-8-0)

[1]: https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwiAvovAk_6EAxVJXUEAHezbAmwQFnoECCwQAQ&url=https%3A%2F%2Fbooks.google.com%2Fbooks%3Fid%3DiIsKzgEACAAJ%26printsec%3Dfrontcover%26source%3Dgbs_atb&usg=AOvVaw3L2E4b--daQTJPSenAp4Q9&opi=89978449
