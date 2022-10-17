using System.Collections.Generic;

namespace StoreApi.Apps.AdminApp.DTOs.CategoryDtos
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public List<CategoryListItemDto> Items { get; set; }

    }
}
