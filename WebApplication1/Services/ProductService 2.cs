using System;
using WebApplication1.Models;
using WebApplication1.Dto.ProductDtos;
using WebApplication1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Transactions;
using WebApplication1.Data;

namespace WebApplication1.Services;

public class ProductService: IProductService
{
    private readonly FirstRunDbContext dbContext;

    public ProductService(FirstRunDbContext dbContext)
    {
        this.dbContext = dbContext;
    }   
    
    public async Task Create(CreateProductDto dto)
    {
        using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var product = new Product();
        product.Name = dto.Name;
        product.Category = dto.ProductCategory;
        product.Description = dto.Description;
        product.Price = 0;
        product.IsAvailable = true;

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        txn.Complete();
    }

    public async Task Update(int ProductId, UpdateProductDto dto)
    {
        using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var product = await dbContext.Products
            .Where(x => x.Id == ProductId)
            .FirstOrDefaultAsync();

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

    public async Task Delete(int ProductId)
    {
        using var txn = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var product = await dbContext.Products
            .Where(x => x.Id == ProductId)
            .FirstOrDefaultAsync();

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        txn.Complete();
    }
}
