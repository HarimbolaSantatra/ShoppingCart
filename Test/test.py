import requests

BASE = "http://localhost:5180/shoppingcart"
HEADERS = {
        "Content-Type": "application/json"
        }

def get_user_cart(user_id: int):
    endpoint = f"{BASE}/{user_id}"
    res = requests.get(endpoint)
    print()
    print(f"=== Getting user {user_id} carts ====")
    return res.text

def get_carts():
    endpoint = f"{BASE}/carts"
    res = requests.get(endpoint)
    print()
    print("=== Getting all carts ====")
    return res.text

def get_items():
    endpoint = f"{BASE}/items"
    res = requests.get(endpoint)
    print()
    print("=== Getting all items ====")
    return res.text

def add_item(user_id: int):
    endpoint = f"{BASE}/{user_id}/item"
    data = {
            "shoppingCartItem": {
                "Id": 1,
                "ProductCatalogueId": 2,
                "ProductName": "testProduct",
                "Description": "This is a test product",
                "Price": 5,
                "CartId": 1,
                }
            }
    res = requests.post(
            endpoint,
            headers=HEADERS,
            data=data
            )
    print()
    print("=== Adding item ====")
    return res.text

def main():
    user_id = 1
    print(get_user_cart(user_id))
    print(get_carts())
    print(get_items())
    print(add_item(user_id))


if __name__ == '__main__':
    main()
