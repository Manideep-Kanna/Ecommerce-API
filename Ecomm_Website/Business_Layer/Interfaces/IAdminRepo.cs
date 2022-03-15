using Data_Access_Layer.Models;
using Data_Access_Layer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public interface IAdminRepo
    {
        /// <summary>
        /// GET: Return the list of Products
        /// </summary>
        /// <returns>List of Products</returns>
        /// 

        public IEnumerable<Product> GetProductS();

        /// <summary>
        /// Updates the Product in the Product table 
        /// </summary>
        /// <param name="requestproduct">Product</param>
        /// <returns>Operation is successfull or not </returns>
        /// 

        public ViewResponseMessage? PutProduct(Product requestproduct);

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="requestcategory">Category</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? PutCategory(Category requestcategory);

        /// <summary>
        /// Gets the list of the Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        /// 

        public List<Category> GetProduct_Category();

        /// <summary>
        /// Get the Category with specific id 
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Category</returns>
        /// 

        public Category? GetProduct_Category(int id);

        /// <summary>
        /// Get the Category with specific Name
        /// </summary>
        /// <param name="name">Category Name</param>
        /// <returns>Category</returns>
        /// 

        public Category? GetProduct_Category(string name);

        /// <summary>
        /// Inserts the product into the Product Table
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? PostProduct(Product product);

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage PostCategory(Category category);

        /// <summary>
        /// Deletes the Product from the Product Table
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? DeleteProduct(int id);

        /// <summary>
        ///  used to Delete the Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Operation is successfull or not</returns>
        /// 

        public ViewResponseMessage? DeleteCategory(int id);
    }
}
