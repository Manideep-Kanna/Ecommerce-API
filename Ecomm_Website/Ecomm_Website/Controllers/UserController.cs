#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Views;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Business_Layer;
namespace Ecomm_Website.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Business_Layer.IUserRepo _context;

        public UserController(IUserRepo context)
        {
            _context = context;
        }

        /// <summary>
        ///  GET: All the Products available in the Product Table
        /// </summary>
        /// <returns>List of Products </returns>

        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<ViewProductDetails>> GetProductS()
        {
            var products=_context.GetProducts();
            if(products == null)BadRequest(new ViewResponseMessage { Message = "No Products Available" });
            return Ok(_context.GetProducts());
        }

        /// <summary>
        /// GET: The Product with the specific Product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>

        [HttpGet("GetProduct/{id}")]
        public ActionResult<ViewProductDetails> GetProduct(int id)
        {
            var product = _context.GetProduct(id);

            if (product == null)
            {
                return BadRequest(new ViewResponseMessage { Message = $"Not Found the Product with ID {id}" });
            }

            return Ok(product);
        }
        /// <summary>
        /// Get the product with specific Name
        /// </summary>
        /// <param name="name">Product Name</param>
        /// <returns>Product</returns>

        [HttpGet("GetProduct/Name/{name}")]
        public ActionResult<ViewProductDetails> GetProduct(string name)
        {
            var product = _context.GetProduct(name);

            if (product == null)
            {
                return BadRequest(new ViewResponseMessage { Message = $"Not Found the Product with Name {name}" });
            }

            return Ok(product); 
        }
        /// <summary>
        /// Get the Products by Category Name
        /// </summary>
        /// <param name="categoryname">Category Name</param>
        /// <returns>List of Products</returns>

        [HttpGet("GetProduct/Category/{categoryname}")]
        public ActionResult<List<ViewProductDetails>> GetProduct_Category(string categoryname)
        {
           var products=_context.GetProduct_Category(categoryname);
            if (products.Count==0)
                return BadRequest(new ViewResponseMessage { Message = $"Not Found the Category with name {categoryname}" });
            return Ok(products);
        }

        /// <summary>
        /// Get the CartItems by UserId
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <returns>List of Cart views</returns>

        [HttpGet("GetAllCartItems/{user_id}")]
        public ActionResult<List<ViewCartItems>> GetProductsInCart(int user_id)
        {
            var products = _context.GetProductsInCart(user_id);
            if (products == null) return BadRequest(new ViewResponseMessage { Message = "There is No User With That specific User Id" });
            return Ok(products);
        }
        /// <summary>
        /// Add the Product to  the Cart
        /// </summary>
        /// <param name="post_product">Product to be added</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpPost("AddProductToCart")]
        public ActionResult<ViewResponseMessage> AddProductToCart(ViewAddProduct post_product)
        {
            var res = _context.AddProductToCart(post_product);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }

        /// <summary>
        /// DELETE: Remove the Product from the Cart
        /// </summary>
        /// <param name="user_id">User ID</param>
        /// <param name="product_id">Product ID</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpDelete("RemoveProductFromCart")]
        public ActionResult<ViewResponseMessage> DeleteProductFromCart(int user_id, int product_id)
        {
            var res = _context.DeleteProductFromCart(user_id, product_id);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }
        /// <summary>
        /// Total Cost of the Cart of specific UserId
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpGet("TotalCostOfCart")]
        public ActionResult<ViewResponseMessage> CostOfItemsInCart(int userId)
        {
            var res = _context.CostOfItemsInCart(userId);
            if (res == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(res);
        }

        /// <summary>
        /// Checkout the Process
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="payment">Cash from User</param>
        /// <returns>Operation is successfull or not</returns>
        [HttpGet("CheckOut")]
        public ActionResult<ViewResponseMessage> CheckOut(int userId,int payment)
        {
            return _context.CheckOut(userId,payment);
        }

        /// <summary>
        /// Add the comments to the product 
        /// </summary>
        /// <param name="comment">Comment to be added</param>
        /// <returns>Operation is successfull or not</returns>

        [HttpPost("AddComment")]
        public ActionResult<ViewResponseMessage> AddComment(Comment comment)
        {
            ViewResponseMessage s = _context.AddComment(comment);
            if (s == null) return BadRequest(new ViewResponseMessage { Message = "There is some wrong with the details you have entered" });
            return Ok(s);
        }

        /// <summary>
        /// Get the Comments for a specific Product
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <returns>Object of Comments</returns>

        [HttpGet("GetComments")]
        public ActionResult<object> GetComments(int ProductId)
        {
            return _context.GetComments(ProductId);
        }

    }
}
