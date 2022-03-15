using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
 using Data_Access_Layer;
using Microsoft.Data.SqlClient;
using Data_Access_Layer.Views;

namespace Business_Layer
{
    /// <summary>
    /// Repository Class for the Admin Controller
    /// </summary>
    public class AdminRepo:IAdminRepo
    {
      private DataContext _context;
      public  AdminRepo(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET: Return the list of Products
        /// </summary>
        /// <returns>List of Products</returns>
        public IEnumerable<Product> GetProductS()
        {
            return _context.Products.ToList();
        }
        /// <summary>
        /// Updates the Product in the Product table 
        /// </summary>
        /// <param name="requestproduct">Product</param>
        /// <returns>Operation is successfull or not </returns>
        public ViewResponseMessage? PutProduct(Product requestproduct)
        {
            var product =  _context.Products.Find(requestproduct.Id);
            if (product == null)
                return null;
            product.Id = requestproduct.Id;
            product.Name = requestproduct.Name;
            product.Price = requestproduct.Price;
            product.Category_Id=requestproduct.Category_Id;
            product.Status= requestproduct.Status;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return new ViewResponseMessage { Message = "Successfully Product Updated" };
        }

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="requestcategory">Category</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? PutCategory(Category requestcategory)
        {
            var category = _context.Categories.Find(requestcategory.Id);
            if (category == null)
                return null;
            category.Id = requestcategory.Id;
            category.Name = requestcategory.Name;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return new ViewResponseMessage { Message = "Successfully Category Updated" };
        }

        /// <summary>
        /// Gets the list of the Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        public List<Category> GetProduct_Category()
        {
            return _context.Categories.ToList();

        }

        /// <summary>
        /// Get the Category with specific id 
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Category</returns>
        public Category? GetProduct_Category(int id)
        {
            Category category;
            try
            {
                category = _context.Categories.Where(ex => ex.Id == id).First();
            }
            catch (Exception)
            {
                return null;
            }
            return category;
        }

        /// <summary>
        /// Get the Category with specific Name
        /// </summary>
        /// <param name="name">Category Name</param>
        /// <returns>Category</returns>
        public Category? GetProduct_Category(string name)
        {
            Category category;
            try
            {
                category = _context.Categories.Where(ex => ex.Name == name).First();
            }
            catch (Exception)
            {
                return null;
            }
            return category;
        }
        /// <summary>
        /// Inserts the product into the Product Table
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? PostProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return null;
            }
      

            return new ViewResponseMessage { Message = $"The Product {product.Name} is inserted" };
        }

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ViewResponseMessage { Message = "The Category {category.Name} is inserted" };
        }

        /// <summary>
        /// Deletes the Product from the Product Table
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return new ViewResponseMessage { Message = $"The product of product_id {id} is removed " };
        }
        /// <summary>
        ///  used to Delete the Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Operation is successfull or not</returns>
        public ViewResponseMessage? DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return null;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return new ViewResponseMessage { Message = $"The category of category_id {id} is removed " };
        }

        /// <summary>
        /// Checks wheather the product exists or not 
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>boolean flag</returns>
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}