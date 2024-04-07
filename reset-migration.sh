#!/bin/bash

db="shopping_cart"
echo "Recreating database ..."
mariadb -h localhost -u root -proot $db -e "DROP DATABASE $db; CREATE DATABASE $db"

echo "removing all migration ..."
rm -rf Migrations/*

# recreate migration
migration_name="InitialCreate"
dotnet ef migrations add $migration_name
dotnet ef database update

