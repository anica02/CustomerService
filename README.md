Firsth thing to do is call get method from DataController so that  the database is created and customer data inserted in database, then call post method from DataController to insert data about users, roles, roleUseCases etc.
User needs to login with the AuthController with credentials email and password to get a jwt, hard-code user is added in DataController and the credentials are Email:"petar@gmail.com" and Password:"sifra123". 
Every use case needs jwt to be executed
In the CustomerDiscountController agent can add a discount to a customer with the post method.
In the PurchaseController the agent on the customer's request added the purchases to the database with post method.
In the CustomerController there is a method that retrieves data from database and creates data for .csv file
