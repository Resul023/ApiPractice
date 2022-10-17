using System.Collections.Generic;

namespace StoreApi.Apps.AdminApp.DTOs.ProductDtos
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public List<ProductListItemDto> Items { get; set; }
    }
}
