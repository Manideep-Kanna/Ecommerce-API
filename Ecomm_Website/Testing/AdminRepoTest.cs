using Business_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class AdminRepoTest
    {
        DataContext _context;
        IAdminRepo AdminRepository;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase("EComDatabase")
           .Options;
            _context = new DataContext(options);
            AdminRepository = new AdminRepo(_context);
            _context.Categories.Add(new Category { Id=1, Name = "laptop" });
            _context.Categories.Add(new Category { Id=2, Name = "mobile" });
            _context.Products.Add(new Product { Id = 1, Name = "HP", Price = 50000, Category_Id = 1 });
            _context.Products.Add(new Product { Id = 2, Name = "DELL", Price = 50000, Category_Id = 1 });
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Dispose();
        }
        [Test]
        [Order(1)]
        public void GetProduct_Category()
        {
            var category = AdminRepository.GetProduct_Category();
            Assert.AreEqual(category.Count, 2);
        }
        [Test]
        [Order(2)]
        public void UpdatecategoryTest()
        {
            var categorydata = _context.Categories.Find(1);
            var categorys = new Category();
            categorys.Name = "shoe";
            AdminRepository.PutCategory(categorys);
            Assert.AreEqual(categorys.Name, "shoe");

        }
        [Test]
        [Order(3)]
        public void DeletecategoryTest()
        {
            AdminRepository.DeleteCategory(2);
            var categories = AdminRepository.GetProduct_Category();
            Assert.AreEqual(categories.Count, 1);

        }
        [Test]
        [Order(4)]
        public void AddCategoryTest()
        {
            AdminRepository.PostCategory(new Category { Id = 2, Name = "mobile" });
            var categories = AdminRepository.GetProduct_Category();
            Assert.AreEqual(categories.Count, 2);
        }
        [Test]
        [Order(5)]
        public void getProductSTest()
        {
            var products = AdminRepository.GetProductS().ToList();
            Assert.AreEqual(products.Count, 2);
        }
        [Test]
        [Order(6)]
        public void PutProductTest()
        {
            var productdata = _context.Products.Find(1);
            var category = new Category();
            category.Name = "Hp";
            AdminRepository.PutCategory(category);
            Assert.AreEqual(category.Name, "Hp");
        }
        [Test]
        [Order(7)]
        public void DelteProductTest()
        {
            AdminRepository.DeleteProduct(2);
            var products=AdminRepository.GetProductS().ToList();
            Assert.AreEqual(products.Count, 1);
        }
        [Test]
        [Order(8)]
        public void AddProductTest()
        {
            AdminRepository.PostProduct(new Product { Id = 2, Name = "DELL", Price = 50000, Category_Id = 1 });
            var products = AdminRepository.GetProductS().ToList();
            Assert.AreEqual(products.Count, 2);
        }
        [Test]
        [Order(9)]
        public void GetCategoryById()
        {
           Category category= AdminRepository.GetProduct_Category(1);
            Assert.AreEqual(category.Name, "laptop");
        }
        [Test]
        [Order(10)]
        public void GetCategoryByNameTest()
        {
            Category category = AdminRepository.GetProduct_Category("laptop");
            Assert.AreEqual(category.Id, 1);
        }

    }
}
