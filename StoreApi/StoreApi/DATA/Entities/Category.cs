using System.Collections.Generic;
using WebApplication1.Entities;

namespace StoreApi.DATA.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
