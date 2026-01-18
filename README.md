
Database Setup (PostgreSQL)
Create the DB:
CREATE DATABASE invoabp;

Configure connection in:
aspnet-core/src/InvoABP.HttpApi/appsettings.json
Example:
"ConnectionStrings": {
  "Default": "Server=localhost;Port=5432;Database=InvoABP;User Id=admin1;Password=Password@123;"
}

Run Database Migrator
This creates tables + seeds data:

cd aspnet-core/src/InvoABP.DbMigrator
dotnet run

Seeding includes:
Admin role
Staff role
Permission grants
User accounts

Seeded Login Accounts
After migration completes:

Role	 Username	 Password
Admin	 admin1	   Password@123
Staff	 staff1	   Password@123

Login endpoint:
POST https://localhost:44366/api/account/login


Example body:
{
  "userNameOrEmailAddress": "admin1",
  "password": "Password@123"
}
Successful response returns:
{
  "result": 1,
  "description": "Success"
}

Permissions Model
Admin can:
Create / Update / Delete Customers
Create / Update / Delete Invoices
Update Invoice Status

Staff can:
View Customers
View Invoices
Create Invoices

Run Backend (API)
cd aspnet-core/src/InvoABP.HttpApi
dotnet run

Backend runs at:
https://localhost:44366/

Swagger is available:
https://localhost:44366/swagger

Configure CORS for UI
Set the origin in:

appsettings.json
"App": {
  "CorsOrigins": "http://localhost:4200"
}

Run Angular Frontend
Navigate to UI directory:

cd angular
npm install

Then:
npx ng serve --open

Angular UI runs at:
http://localhost:4200/

