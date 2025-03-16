using System;
using WebApplication1.Dto.ProductDtos;

namespace WebApplication1.Services.Interfaces;

public interface IProductService
{
    //Create
    Task Create(CreateProductDto dto);

    //Update
    Task Update(int productId, UpdateProductDto dto);

    //Delete
    Task Delete(int productId);
}
