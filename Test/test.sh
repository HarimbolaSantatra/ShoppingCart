#!/bin/bash

port=5180
base="http://localhost:$port/shoppingCart"

userid=1

data="{ \"cartId\":1, \"itemId\":1 }"

curl -XPOST \
    -s \
    -H "Content-Type: application/json" \
    -d $data \
    $base/$userid/item
