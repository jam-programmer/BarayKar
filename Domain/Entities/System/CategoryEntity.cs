
using Domain.Entities.Business;

namespace Domain.Entities.System
{
    public class CategoryEntity:BaseEntity
    {
        public string? Image { set; get; }
        public required string Name { set;get; }
        public Guid? ParentCategoryId { get; set; }
        public CategoryEntity? ParentCategory { get; set; }

        public ICollection<CategoryEntity>? ChildCategories { get; set; } 

        public ICollection<BusinessEntity>? Businesses { get; set; }

    }
}
