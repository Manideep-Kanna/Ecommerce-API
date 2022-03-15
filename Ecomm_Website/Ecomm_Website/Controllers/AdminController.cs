#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Business_Layer;
using Data_Access_Layer.Views;

namespace Ecomm_Website.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _context;

        public AdminController(IAdminRepo context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Return the list of Products
        /// </summary>
        /// <returns>List of Products</returns>

        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetProductS()
        {
            var products = _context.GetProductS();
            if (products == null) BadRequest(new ViewResponseMessage { Message = "No Products Available" });
            return Ok(products);
        }

        /// <summary>
        /// Updates the Product in the Product table 
        /// </summary>
        /// <param name="requestproduct">Product</param>
        /// <returns>Operation is successfull or not </returns>

        [HttpPut("UpdateProduct/Id")]
        public ActionResult<ViewResponseMessage> PutProduct( Product product)
        {
           var res= _context.PutProduct(product);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }

        /// <summary>
        /// Inserts the product into the Product Table
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Operation is successfull or not</returns>
        [HttpPost("AddProduct")]
        public ActionResult<ViewResponseMessage> PostProduct(Product product)
        {
            var res = _context.PostProduct(product);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }
        /// <summary>
        /// Deletes the Product from the Product Table
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<ViewResponseMessage> DeleteProduct(int id)
        {
            var res = _context.DeleteProduct(id);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }

        /// <summary>
        /// Gets the list of the Categories
        /// </summary>
        /// <returns>List of Categories</returns>

        [HttpGet("GetAllCategories")]
        public ActionResult<IEnumerable<Category>> GetProduct_Category()
        {
            var categories = _context.GetProduct_Category();
            if (categories == null) BadRequest(new ViewResponseMessage { Message = "No Products Available" });
            return Ok(categories);
        }

        /// <summary>
        /// Get the Category with specific id 
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Category</returns>

        [HttpGet("GetCategory/Id")]
        public ActionResult<Category> GetProduct_Category(int id)
        {
            var category = _context.GetProduct_Category(id);
            if (category == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(category);
        }

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="requestcategory">Category</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpPut("UpdateCategory/Id")]
        public ActionResult<ViewResponseMessage> PutCategory(Category category)
        {
            var res = _context.PutCategory(category);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }

        /// <summary>
        /// Get the Category with specific Name
        /// </summary>
        /// <param name="name">Category Name</param>
        /// <returns>Category</returns>

        [HttpGet("GetCategory/{name}")]
        public ActionResult<Category> GetProduct_Category(string name)
        {
            var category = _context.GetProduct_Category(name);
            if (category == null)return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(category);
        }

        /// <summary>
        /// Inserts the Category into the Category Table
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpPost("AddCategory")]
        public ActionResult<ViewResponseMessage> PostCategory(Category reqcategory)
        {

            var category = _context.PostCategory(reqcategory);
            if (category == null)return  BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(category);
        }

        /// <summary>
        ///  used to Delete the Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpDelete("DeleteCategory/{id}")]
        public ActionResult<ViewResponseMessage> DeleteCategory(int id)
        {
            var category = _context.DeleteCategory(id);
            if (category == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(category);
        }

    }
}
