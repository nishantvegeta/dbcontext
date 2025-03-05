using System;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.ViewModels.ProductVms;

public class UpdateProductVm
{
    // send the product categories from view to controller
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public string Description { get; set; }

        public SelectList ProductCategoriesSelectList() 
            => new SelectList(
                // List of ProductCategories
                ProductCategories, 
                // Value
                nameof(ProductCategory.Id), 
                //display text
                nameof(ProductCategory.Name),
                ProductCategoryId
            );
        public List<ProductCategory> ProductCategories;
}
