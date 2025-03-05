using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Column("id")] // [key]

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}