# HD.API
This is an .NET 7 Minimal API that queries data from Home Depot's servers. Currently you can pull product data by Product ID. 

This API is built an tested on a Kestrel Server. However, this is not production ready for any real-world use cases as I only have a single API endpoint.

# Usage
Once the server is running the only endpoint available is /GetProductByID/{productID}

# Future Development
I will eventually add many more endpoints as crafting queries is as simple as building a MathQL query matching Home Depot's Schema. Flexability in assigning variables 
in the Http Request opens up a lot of use cases. 

My original intent with this project is to develop a Barcode Scanner Application that can search product by UPC while in store. With eventually linking this API with Contractors+ 
API endpoints to build estimates, shopping lists, expense entries, etc. 
