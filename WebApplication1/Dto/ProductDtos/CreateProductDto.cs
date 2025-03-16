using System;
using WebApplication1.Models;

namespace WebApplication1.Dto.ProductDtos;

public class CreateProductDto
{
    public string Name {get; set;}
    public ProductCategory ProductCategory {get; set;}
    public string Description {get; set;}
}
