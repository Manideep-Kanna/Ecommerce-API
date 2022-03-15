using Business_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Data_Access_Layer.Views;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Testing
{
    public class Tests
    {
        DataContext _context;
        UserRepo UserRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase("EComDatabase")
           .Options;
            _context = new DataContext(options);
            UserRepository = new UserRepo(_context);
            _context.Categories.Add(new Category { Id = 1, Name = "laptop" });
            _context.Categories.Add(new Category { Id = 2, Name = "mobile" });
            _context.Products.Add(new Product { Id = 1, Name = "HP", Price = 50000, Category_Id = 1 });
            _context.Products.Add(new Product { Id = 2, Name = "DELL", Price = 50000, Category_Id = 1 });
            _context.Products.Add(new Product { Id = 3, Name = "I PAD", Price = 70000, Category_Id = 2 });
            _context.Carts.Add(new Cart { CartId = 1, User_Id = 1 });
            _context.Carts.Add(new Cart { CartId = 2, User_Id = 2 });
            _context.Cart_Items.Add(new Cart_Item { Id = 1, Cart_Id = 1, Product_Id = 2, Count = 1 });
            _context.Cart_Items.Add(new Cart_Item { Id = 2, Cart_Id = 1, Product_Id = 1, Count = 2 });
            _context.Users.Add(new User { Id = 1, Name = "Kanna" });
            _context.Users.Add(new User { Id = 2, Name = "Karthik" });
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetProductTest()
        {
            var product=UserRepository.GetProduct(1);
            Assert.AreEqual(product.Name, "HP");
        }
        [Test]
        [Order(2)]
        public void GetProductByNameTest()
        {
            var product = UserRepository.GetProduct("DELL");
            Assert.AreEqual(product.Id, 2);
        }
        [Test]
        [Order(3)]
        public void GetProduct_CategoryTest()
        {
            var product = UserRepository.GetProduct_Category("laptop");
            Assert.AreEqual(product.Count, 2);
        }
        [Test]
        [Order(4)]
        public void GetProductsInCartTest()
        {
            var product = UserRepository.GetProductsInCart(1);
            Assert.AreEqual(product.Count, 2);
        }
        [Test]
        [Order(5)]
        public void GetProductInCartTest()
        {
            var product = UserRepository.GetProductsInCart(1);
            Assert.AreEqual(product.Count, 2);
        }
        [Test]
        [Order(6)]
        public void AddProductToCartTest()
        {
            UserRepository.AddProductToCart(new ViewAddProduct { UserId = 1, ProductId = 3 });
            var products = UserRepository.GetProductsInCart(1);
            Assert.AreEqual(products.Count, 3);
        }
        [Test]
        [Order(7)]
        public void DeleteProductFromCartTest()
        {
            UserRepository.DeleteProductFromCart(1, 3);
            var products = UserRepository.GetProductsInCart(1);
            Assert.AreEqual(products.Count, 2);
        }
        [Test]
        [Order(8)]
        public void CostOfItemsInCartTest()
        {
            var cost = UserRepository.CostOfItemsInCart(1);
            Assert.AreEqual(cost.Message, "The total cost of the items are 150000");
        }
        [Test]
        [Order(9)]
        public void CheckOut()
        {
            UserRepository.CheckOut(1, 200000);
            var products = UserRepository.GetProductsInCart(1);
            Assert.AreEqual(products.Count, 0);
        }
    }
}