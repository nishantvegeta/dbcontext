using System;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }
    }
}