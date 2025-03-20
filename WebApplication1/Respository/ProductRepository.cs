using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repository.Interfaces;
using WebApplication1.ViewModels.ProductVms;

namespace WebApplication1.Repository
{
    public class Repository : IRepository
    {
        private readonly FirstRunDbContext dbContext;

        public Repository(FirstRunDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all products
        public async Task<IEnumerable<ProductVm>> GetAllProducts()
        {
            return await dbContext.Products
                .AsNoTracking()
                .Select(p => new ProductVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.CategoryId,
                    Description = p.Description
                })
                .ToListAsync();
        }

        // Get product by ID
        public async Task<ProductVm> GetProductById(int id)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.CategoryId,
                Description = product.Description
            };
        }

        // Search products by name
        public async Task<IEnumerable<ProductVm>> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<ProductVm>(); // Return empty list if search term is empty
            }

            return await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchTerm))
                .Select(p => new ProductVm
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.CategoryId,
                    Description = p.Description
                })
                .ToListAsync();
        }
    }
}
