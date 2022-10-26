using System.Collections.Generic;

namespace StoreMVC.DTOs
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public List<CategoryListItemDto> Items { get; set; }

    }
    public class CategoryListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
