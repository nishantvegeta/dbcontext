using System;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.ProductVms
{
    public class CreateProductVm
    {
        // send the product categories from view to controller
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public string Description { get; set; }

        // send to view the list of product categories
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
}