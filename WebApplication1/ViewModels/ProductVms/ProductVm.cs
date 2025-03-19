using System;
using WebApplication1.Models;

namespace WebApplication1.ViewModels.ProductVms;

public class ProductVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Category { get; set; }

    public List<Product> Products;
}
