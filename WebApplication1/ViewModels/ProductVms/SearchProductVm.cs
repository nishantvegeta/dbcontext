using System;

using WebApplication1.Models;

namespace WebApplication1.ViewModels.ProductVms;

public class SearchProductVm
{
    public string Name {get;set;}

    public List<Product> Products;
}
