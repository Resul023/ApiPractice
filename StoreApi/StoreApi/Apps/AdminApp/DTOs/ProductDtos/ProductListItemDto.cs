namespace StoreApi.Apps.AdminApp.DTOs.ProductDtos
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal SalePrice { get; set; }
        public CategoryInProductListItemDto Category { get; set; }
    }
    public class CategoryInProductListItemDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }


}
