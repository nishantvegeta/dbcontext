using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;
using System;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class ApiTest : Controller
    {
        private readonly FirstRunDbContext dbContext;

        //DI
        public ApiTest(FirstRunDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("api/product/seed")]
        public async Task<IActionResult> SeedProductCategory()
        {
            var productCategory = new ProductCategory
            {
                Name = "Test Category",
                IsAvailable = true,
                CreatedAt = System.DateTime.UtcNow
            };

            dbContext.ProductCategories.Add(productCategory);

            await dbContext.SaveChangesAsync();

            return Ok(productCategory);
        }


        //get list
        [HttpGet("api/product/list")]
        public async Task<IActionResult> GetProductCategory()
        {
            var productCategories = await dbContext.ProductCategories.ToListAsync();

            return Ok(productCategories);
        }

        //update
        [HttpGet("api/product/update/{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id)
        {
            var productCategory = await dbContext.ProductCategories.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (productCategory == null)
            {
                return NotFound();
            }

            productCategory.Name = "Updated Category";

            dbContext.ProductCategories.Update(productCategory);

            await dbContext.SaveChangesAsync();

            return Ok(productCategory); //return updated product category
        }

        // foriegn key
        [HttpGet("api/product/seed-product/{id}")]
        public async Task<IActionResult> CreateSampleProduct(int id)
        {
            var productCategory = await dbContext.ProductCategories.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (productCategory == null)
            {
                return NotFound();
            }

            var product = new Product();
            product.Name = "Test Product";
            product.Price = 100;
            product.Description = "Test Description";
            product.IsAvailable = true;
            product.Category = productCategory;

            dbContext.Products.Add(product);

            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        //get product list
        [HttpGet("api/product/list-product")]
        public async Task<IActionResult> GetProduct()
        {
            var products = await dbContext.Products.ToListAsync();

            return Ok(products);
        }
    }
}