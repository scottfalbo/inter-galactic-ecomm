# Welcome to InterGalactic-Ecomm!

## Authors:

Scott Falbo | Matthew Petersen

*version 1.0.1*

---

## Overview:

https://intergalacticecomm.azurewebsites.net

We are in the process of creating an E-commerce website, selling Rick and Morty memorabilia. Our store is currently comprised of Products, ProductCategories, and Categories.
Products are tied to Categories by ProductCategories, which is a join table. This means that you can click on a category, and see all the products that belong to it!

Users are able to register and login with a simple username and password. 
If a user is logged in as an **Admin**, the user is able to perform CRUD operations on the Products and Categories. This is enforced by a Policy to ensure that guests aren't tampering with our website.


---

## Getting Started
+ `git clone https://scottfalboart@dev.azure.com/scottfalboart/InterGalactic-Ecomm/_git/InterGalactic-Ecomm`
+ Open the project in Visual Studio or compile it from the command line.
+ You can successfully register an account via the register link on the home page.  Once registered you will be redirected to login.
  + Then nothing happens...

## Example

![Registration and Login](assets/signin.png)

---

## Architecture

  <img src ="https://img.shields.io/badge/C%23%20-%23239120.svg?style=flat&logo=c%2B%2B&logoColor=ffffff">
  <img src="https://img.shields.io/badge/.NET Core-net%23239120.svg?style=flat&logo=dot-net&logoColor=00c8ff">
  <img src="https://img.shields.io/badge/Azure%20-%230072C6.svg?style=flat&logo=azure-devops&logoColor=00c8ff">

This is an ASP.Net Core App built in an MVC framework, that utilizes Microsoft Framework Identity to register and authenticate users.  

Product and Category data is stored in a SQL database using NewtonSoftJson to parse the json.

CategoryProduct is a join table which connects Products to Categories


## ERD
![Whiteboard](assets/intergalactic_erd.PNG)

### Change Log:
+ *02/15/2021* - Scaffolded out files, started working on jwt and user login. Seeded products and categories to db.
+ *02/16/2021* - Brought in Identity dependencies.
+ Created user roles and policies.  Added permissions to routes.
+ Made forms for registering a user, and then subsequently logging in as that user.
+ *02/18/2021* - Added blobs so users can add/update images to products. Fixed all CRUD routes. Products can now be added to categories.
+ *02/19/2021* - Deployed web app, added summary comments, fixed CRUD operations so only an Admin can view the forms.
+ *02/22/2021* - Added razor pages for user interaction. All routes operational. Admin routes are all locked down.
+ *02/23/2021* - 
  + Added `Cart` and `CartProduct`join table models.
  + Created `ShoppingCart` view.
  + Added crud to put products into the shopping cart
+ *02/24/2021* - 
  + Added `Order` model
  + Implemented components for the cart which tracks item count and login/logout nav.
  + When order now button in the cart is clicked a new order is created and stored in the DB.
  + Added some basic style here and there.
+ *02/25/2021* - Emails are sent to the user, admin and warehouse after purchase. The users cart is also emptied after purchase.
---

## Attribution
+ As always thank you to John Cokos, Bade Habib, and Phil Werner for the instruction and assists.