using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST /api/products
        [HttpPost("products")]
         public ActionResult<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }


        // PUT /api/products/{id}
        [HttpPut("products/{id}")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedProduct).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("products/{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }

        // // GET /api/products/sortbyprice
        // [HttpGet("products/sortbyprice")]
        // public ActionResult<IEnumerable<Product>> GetProductsSortedByPrice()
        // {
        //     var products = _context.Products.OrderBy(p => p.Price).ToList();
        //     return products;
        // }

        // // GET /api/products/averageprice
        // [HttpGet("products/averageprice")]
        // public ActionResult<Product> GetProductWithAveragePrice()
        // {
        //     decimal averagePrice = _context.Products.Average(p => p.Price);
        //     var product = _context.Products.FirstOrDefault(p => p.Price == averagePrice);
        //     return product;
        // }

        // // GET /api/products/highestprice
        // [HttpGet("products/highestprice")]
        // public ActionResult<Product> GetProductWithHighestPrice()
        // {
        //     var product = _context.Products.OrderByDescending(p => p.Price).FirstOrDefault();
        //     return product;
        // }

        // // GET /api/products/lowestprice
        // [HttpGet("products/lowestprice")]
        // public ActionResult<Product> GetProductWithLowestPrice()
        // {
        //     var product = _context.Products.OrderBy(p => p.Price).FirstOrDefault();
        //     return product;
        // }

        // // GET /api/categories/highestproductcount
        // [HttpGet("categories/highestproductcount")]
        // public ActionResult<string> GetCategoryWithHighestProductCount()
        // {
        //     var category = _context.Categories
        //         .OrderByDescending(c => c.Products.Count)
        //         .Select(c => c.Name)
        //         .FirstOrDefault();
        //     return category;
        // }

        // // GET /api/categories/sortbyname
        // [HttpGet("categories/sortbyname")]
        // public ActionResult<IEnumerable<Category>> GetCategoriesSortedByName()
        // {
        //     var categories = _context.Categories.OrderBy(c => c.Name).ToList();
        //     return categories;
        // }

        // // POST /api/categories
        // [HttpPost("categories")]
        // public ActionResult<Category> CreateCategory(Category category)
        // {
        //     _context.Categories.Add(category);
        //     _context.SaveChanges();
        //     return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
        // }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        // private bool CategoryExists(int id)
        // {
        //     return _context.Categories.Any(c => c.Id == id);
        // }
    }
}
