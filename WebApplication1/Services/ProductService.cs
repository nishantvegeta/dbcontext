using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto.ProductDtos;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;
using System.Transactions;

namespace WebApplication1.Services
{
    public class ProductService : IProductService
    {
        private readonly FirstRunDbContext dbContext;

        public ProductService(FirstRunDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(CreateProductDto dto)
        {
            using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            // Check for duplicates
            var exists = await dbContext.Products
                .AnyAsync(x => x.Name == dto.Name);

            if (exists)
            {
                throw new Exception("Already Exists");
            }
            var product = new Product();
            product.Name = dto.Name;
            product.Category = dto.ProductCategory;
            product.Description = dto.Description;

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            txn.Complete();
        }

        public async Task Update(int productId, UpdateProductDto dto)
        {
            using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await dbContext.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = dto.Name;
            product.Category = dto.ProductCategory;
            product.Description = dto.Description;

            dbContext.Products.Update(product);

            await dbContext.SaveChangesAsync();
            txn.Complete();
        }

        public async Task Delete(int productId)
        {
            using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var product = await dbContext.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            txn.Complete();
        }
    }
}