using System;
using WebApplication1.Dto.ProductDtos;

namespace WebApplication1.Services.Interfaces
{
    public interface IProductService
    {
        Task Create(CreateProductDto Dto);

        Task Update(int productId, UpdateProductDto Dto);

        Task Delete(int productId);
    }
}