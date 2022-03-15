# Sapient_Ecommerce_API
The following API is an clone for the Amazon or Flipkart website it contains all of the basic functionalities what these websites contain.But we have not created an website we just wanted to create an REST full service so anyone can use these functionalities to include them in their own website.

The code itself is production ready and fully documented so even by seeing code you can get an idea of what is actually happening .

We have written **unit test cases** for each controller functionality.

As we know we dont have an frontend for APIs but we have used **swagger** which is actually an Open documented API which specifies all of the functionalities in an page. We can see each functionality as below photos.

We have also deployed this API in **Azure** during testing wheather it is working or not in an real world environment,It worked fine.

So we have divided this API into two parts 
1.Admin
2.User


**Admin:**
Admin can insert products,categories he can able to do all CRUD operations on these. the relationship between tables is given by foreign key 

![Screenshot (58)](https://user-images.githubusercontent.com/72699920/158395765-1ce1bcf4-ed68-4cf2-b352-d1961ec1e976.png)

**User:**
User can see the products and add items to his carts,remove items from his cart he could checkout the products that he added in his cart and he also can able to add the comments to the product.

![Screenshot (59)](https://user-images.githubusercontent.com/72699920/158396199-0d965de9-61f6-4139-b94e-bb41e97b7e18.png)

**Database Diagram:**

![Screenshot (60)](https://user-images.githubusercontent.com/72699920/158396324-b4564fcf-db59-400d-a9f4-dcccd022546a.png)


**Note**:As we know our information is stored in database even though we have checked out in amazon and flipkart so we have imitated that process maintained an backup database which keeps track of the orders of the user.



