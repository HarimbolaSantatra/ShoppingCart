#!/bin/bash
set -e

db="shopping_cart"
username="root"
passwd="root"

echo "==== Recreating database ..."
mariadb -u $username -p${passwd} $db -e "DROP DATABASE $db; CREATE DATABASE $db"

echo "==== Removing all migration ..."
rm -rf Migrations/*

# recreate migration
echo "===== Recreating migrations ..."
migration_name="InitialCreate"
dotnet ef migrations add $migration_name
dotnet ef database update

echo "==== Recreating the initial user ..."
mariadb -u $username -p${passwd} $db -e "INSERT INTO User (Username) VALUES ('santatra')"
