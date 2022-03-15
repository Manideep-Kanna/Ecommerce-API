using Data_Access_Layer.Models;
using Data_Access_Layer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface IUserRepo
    {

        /// <summary>
        ///  GET: All the Products available in the Product Table
        /// </summary>
        /// <returns>List of Products </returns>
        /// 
        public List<ViewProductDetails> GetProducts();

        /// <summary>
        /// GET: The Product with the specific Product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>
        /// 
        public ViewProductDetails? GetProduct(int id);

        /// <summary>
        /// Get the product with specific Name
        /// </summary>
        /// <param name="name">Product Name</param>
        /// <returns>Product</returns>
        /// 
        public ViewProductDetails? GetProduct(string name);

        /// <summary>
        /// Get the Products by Category Name
        /// </summary>
        /// <param name="categoryname">Category Name</param>
        /// <returns>List of Products</returns>
        /// 

        public List<ViewProductDetails> GetProduct_Category(string categoryname);

        /// <summary>
        /// Get the CartItems by UserId
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <returns>List of Cart views</returns>
        /// 

        public List<ViewCartItems>? GetProductsInCart(int user_id);

        /// <summary>
        /// Add the Product to  the Cart
        /// </summary>
        /// <param name="post_product">Product to be added</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? AddProductToCart(ViewAddProduct post_product);

        /// <summary>
        /// DELETE: Remove the Product from the Cart
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <param name="product_id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? DeleteProductFromCart(int user_id, int product_id);

        /// <summary>
        /// Total Cost of the Cart of specific UserId
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? CostOfItemsInCart(int userId);

        /// <summary>
        /// Checkout the Process
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="payment">Cash from User</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? CheckOut(int userId, int payment);

        /// <summary>
        /// Add the comments to the product 
        /// </summary>
        /// <param name="comment">Comment to be added</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage AddComment(Comment comment);

        /// <summary>
        /// Get the Comments for a specific Product
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <returns>Object of Comments</returns>
        /// 

        public object GetComments(int ProductId);

        /// <summary>
        /// Get Replies for an particular comment
        /// </summary>
        /// <param name="comments">List of Comments</param>
        /// <param name="ParentId">Parent Id</param>
        /// <returns>List of ViewComment</returns>
        public List<ViewComment> getReplies(List<Comment> comments, int ParentId);
    }
}
