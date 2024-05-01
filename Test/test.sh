#!/bin/bash

port=5180
base="http://localhost:$port/shoppingCart"

curl -XPOST \
    -s \
    -H "Content-Type: application/json" \
    -d '{ "name":"walk dog", "isComplete":true }' \
    $base/test
