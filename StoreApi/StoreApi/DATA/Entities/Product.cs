using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApi.DATA.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
        public decimal SalePrice { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }

        
    }
}
