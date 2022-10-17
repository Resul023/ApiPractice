namespace StoreApi.Apps.AdminApp.DTOs.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }   
        public decimal CostPrice { get; set; }
    }
}
