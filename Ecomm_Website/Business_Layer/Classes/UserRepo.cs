using Data_Access_Layer.Views;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Business_Layer
{
    public class UserRepo:IUserRepo
    {
        private DataContext _context;
        public UserRepo(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  GET: All the Products available in the Product Table
        /// </summary>
        /// <returns>List of Products </returns>
        public List<ViewProductDetails> GetProducts()
        {
            var products = (from product in _context.Products
                            join category in _context.Categories on product.Category_Id equals category.Id
                            where product.Status=="Active"
                            select new ViewProductDetails
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Price = product.Price,
                                Category_Name = category.Name
                            }).ToList();
            return products;
        }

        /// <summary>
        /// GET: The Product with the specific Product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>
        public ViewProductDetails? GetProduct(int id)
        {
            ViewProductDetails Product;
            try
            {
                Product = (from product in _context.Products
                            join category in _context.Categories on product.Category_Id equals category.Id 
                           where product.Id==id && product.Status == "Active"
                            select new ViewProductDetails
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Price = product.Price,
                                Category_Name = category.Name
                            }).FirstOrDefault();
            }

            catch (Exception)
            {
                return null;
            }
            return Product;
        }

        /// <summary>
        /// Get the product with specific Name
        /// </summary>
        /// <param name="name">Product Name</param>
        /// <returns>Product</returns>
        public ViewProductDetails? GetProduct(string name)
        {
            ViewProductDetails Product;
            try
            {
                Product = (from product in _context.Products
                           join category in _context.Categories on product.Category_Id equals category.Id
                           where product.Name==name && product.Status == "Active"
                           select new ViewProductDetails
                           {
                               Id = product.Id,
                               Name = product.Name,
                               Price = product.Price,
                               Category_Name = category.Name
                           }).FirstOrDefault();
            }

            catch (Exception)
            {
                return null;
            }
            return Product;
        }

        /// <summary>
        /// Get the Products by Category Name
        /// </summary>
        /// <param name="categoryname">Category Name</param>
        /// <returns>List of Products</returns>
        public List<ViewProductDetails> GetProduct_Category(string categoryname)
        {
           List< ViewProductDetails> Product;
            try
            {
                Product = (from product in _context.Products
                           join category in _context.Categories on product.Category_Id equals category.Id
                           where category.Name == categoryname && product.Status == "Active"
                           select new ViewProductDetails
                           {
                               Id = product.Id,
                               Name = product.Name,
                               Price = product.Price,
                               Category_Name = category.Name
                           }).ToList();
            }

            catch (Exception)
            {
                return null;
            }
            return Product;
        }
        /// <summary>
        /// Get the CartItems by UserId
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <returns>List of Cart views</returns>
        public List<ViewCartItems>? GetProductsInCart(int user_id)
        {
            int cartid = getCartId(user_id);
            List<ViewCartItems>? products = null;
            if (cartid != -1)
            {
                products = (from product in _context.Products
                            join cart_item in _context.Cart_Items
                            on product.Id equals cart_item.Product_Id
                            where cart_item.Cart_Id == cartid
                            select new ViewCartItems
                            {
                                Name = product.Name,
                                count = (int)cart_item.Count,
                                Cost = (int)cart_item.Count * _context.Products.Where(ex => ex.Id == cart_item.Product_Id).FirstOrDefault().Price

                            }
                               ).ToList();
            }
            return products;


        }

        /// <summary>
        /// Add the Product to  the Cart
        /// </summary>
        /// <param name="post_product">Product to be added</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? AddProductToCart(ViewAddProduct post_product)
        {
            if (!Product_Exists(post_product.ProductId))
            {
                return null;
            }
            int cartid = getCartId(post_product.UserId);
            if (cartid == -1) return null;
            int productid = post_product.ProductId;
            if (OrderExists(cartid, productid))
            {
                Cart_Item cart_item = getCartItem(cartid, productid);
                cart_item.Count = cart_item.Count + 1;
            }
            else
            {
                var cart_item = new Cart_Item
                {
                    Cart_Id = cartid,
                    Product_Id = productid,
                    Count = 1
                };
                _context.Cart_Items.Add(cart_item);
            }
            _context.SaveChangesAsync();

            return new ViewResponseMessage { Message = "The Order  is added into the cart" };
        }

        /// <summary>
        /// DELETE: Remove the Product from the Cart
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <param name="product_id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? DeleteProductFromCart(int user_id, int product_id)
        {
            int cartid = getCartId(user_id);
            if (cartid == -1) return null;
            if (OrderExists(cartid, product_id))
            {
                Cart_Item cart_item = getCartItem(cartid, product_id);
                cart_item.Count = cart_item.Count - 1;
                if (cart_item.Count == 0) _context.Cart_Items.Remove(cart_item);
            }
            else
            {
                return null;
            }
            _context.SaveChangesAsync();
            return new ViewResponseMessage { Message = "Product is successfully removed from the cart" };
        }

        /// <summary>
        /// Total Cost of the Cart of specific UserId
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? CostOfItemsInCart(int userId)
        {
            int cart_id = getCartId(userId);
            if (cart_id == -1) return null;
            int cost = CostOfCart(cart_id);

            return new ViewResponseMessage { Message = $"The total cost of the items are {cost}" };
        }

        /// <summary>
        /// Checkout the Process
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="payment">Cash from User</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? CheckOut(int userId,int payment)
        {
            int cart_id = getCartId(userId);
            if (cart_id == -1) return null;
            int totalCost = CostOfCart(cart_id);
            if(payment<totalCost) return new ViewResponseMessage { Message = "Insufficient Fund Coudnt Proceed with your Checkout order" };
            payment -= totalCost;
            IList<Cart_Item> cart_items = _context.Cart_Items.Where(ex => ex.Cart_Id == cart_id).ToList();
            foreach (var cart_item in cart_items)
            {
                _context.Orders.Add(new Order
                {
                    UserId = userId,
                    ProductId = cart_item.Product_Id,
                    Count = cart_item.Count
                });

            }
            _context.Cart_Items.RemoveRange(cart_items);
            _context.SaveChanges();
            string res="";
            if (payment > 0) res = $"Here is your Remaining Balance {payment}";
              res+= "Payment Successfull Please Visit our store again";
            return new ViewResponseMessage { Message=res};

        }

        /// <summary>
        /// Add the comments to the product 
        /// </summary>
        /// <param name="comment">Comment to be added</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return new ViewResponseMessage { Message = "Successfully added the comment" };
        }

        /// <summary>
        /// Get the Comments for a specific Product
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <returns>Object of Comments</returns>
        public object GetComments(int ProductId)
        {
            List<Comment> comments = _context.Comments.ToList();
           return  comments.Where(ex=>ex.ProductId==ProductId&& ex.ParentId == null).Select(ex=>new ViewComment
            {
                Id=ex.Id,
                ProductId=ex.ProductId,
                UserId=ex.UserId,
                ParentId=ex.ParentId,
                Comment_Text=ex.Comment_Text,
                Replies=getReplies(comments,ex.Id)

            }).ToList();
        }

        /// <summary>
        /// Get Replies for an particular comment
        /// </summary>
        /// <param name="comments">List of Comments</param>
        /// <param name="ParentId">Parent Id</param>
        /// <returns></returns>
        public List<ViewComment> getReplies(List<Comment>comments,int ParentId)
        {
           return comments.Where(ex => ex.ParentId == ParentId).Select(ex => new ViewComment
            {
                Id = ex.Id,
                ProductId = ex.ProductId,
                UserId = ex.UserId,
                ParentId = ex.ParentId,
                Comment_Text = ex.Comment_Text,
                Replies = getReplies(comments,ex.Id)

            }).ToList();
        }

        /// <summary>
        /// Checks weather the orderExists 
        /// </summary>
        /// <param name="cartid">Cart Id</param>
        /// <param name="productid">Product Id</param>
        /// <returns>boolean flag</returns>
        private bool OrderExists(int cartid, int productid)
        {
            return _context.Cart_Items.Any(ex => ex.Cart_Id == cartid && ex.Product_Id == productid);
        }

        /// <summary>
        /// Get the Cart Id from the User Id
        /// </summary>
        /// <param name="user_Id">User Id</param>
        /// <returns>Cart Id</returns>
        private int getCartId(int user_Id)
        {
            Cart cart;
            try
            {
                cart = _context.Carts.Where(ex => ex.User_Id == user_Id).First();
            }
            catch (Exception)
            {
                return -1;
            }
            return cart.CartId;

        }

        /// <summary>
        /// Gets the ProductId from Product Name
        /// </summary>
        /// <param name="product_name">Product Name</param>
        /// <returns>Product Id</returns>
        private int getProductId(string product_name)
        {
            return _context.Products.Where(ex => ex.Name == product_name).FirstOrDefault().Id;
        }

        /// <summary>
        /// Get the User Id from User Name
        /// </summary>
        /// <param name="user_name">User Name</param>
        /// <returns>User Id</returns>
        private int getUserId(string user_name)
        {
            return _context.Users.Where(ex => ex.Name == user_name).FirstOrDefault().Id;
        }

        /// <summary>
        /// Gives the Cart_Item 
        /// </summary>
        /// <param name="cartid">Cart Id</param>
        /// <param name="productid">Product Id</param>
        /// <returns>Cart_Item</returns>
        private Cart_Item getCartItem(int cartid, int productid)
        {
            return _context.Cart_Items.Where(ex => ex.Product_Id == productid && ex.Cart_Id == cartid).FirstOrDefault();
        }

       /// <summary>
       /// Checks weather the product exists or not 
       /// </summary>
       /// <param name="Product_Id"></param>
       /// <returns></returns>
        private bool Product_Exists(int Product_Id)
        {
            var product = _context.Products.Find(Product_Id);
            if (product == null) return false;
            return true;
        }

        /// <summary>
        /// Get the User Id
        /// </summary>
        /// <param name="cart_id">Cart ID</param>
        /// <returns>User Id</returns>
        private int getUserId(int cart_id)
        {
            return _context.Carts.Find(cart_id).User_Id;
        }

        /// <summary>
        /// Returns the Cost of the Cart
        /// </summary>
        /// <param name="cartid">Cart Id</param>
        /// <returns>Cost of the cart</returns>
        private int CostOfCart(int cartid)
        {
            int cost = 0;
            var product_list = _context.Cart_Items.Where(ord => ord.Cart_Id == cartid).ToList();
            foreach (var prod in product_list)
            {
                var product_from_Product_Table = _context.Products
                    .Where(ex => ex.Id == prod.Product_Id).FirstOrDefault();
                cost += (int)(product_from_Product_Table.Price * prod.Count);
            }
            return cost;
        }
    }
}
