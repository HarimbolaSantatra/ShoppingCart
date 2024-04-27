import requests

BASE = "http://localhost:5180/shoppingcart"
HEADERS = {
        "Content-Type": "application/json"
        }

def get_user_cart(user_id: int):
    endpoint = f"{BASE}/{user_id}"
    res = requests.get(endpoint)
    print()
    print("=== Getting user carts ====")
    return res.text

def get_carts():
    endpoint = f"{BASE}/carts"
    res = requests.get(endpoint)
    print()
    print("=== Getting carts ====")
    return res.text

def add_item(user_id: int):
    endpoint = f"{BASE}/{user_id}/item"
    data = {
            "Id": 1,
            "ProductCatalogueId": 2,
            "ProductName": "testProduct",
            "Description": "This is a test product",
            "Price": 5,
            "CartId": 1,
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
    print(add_item(user_id))


if __name__ == '__main__':
    main()
