namespace Application.Common.ViewModel.Home
{
    public class CategoryInfoViewModel
    {
        public string? Image { set; get; }
        public required string Name { set; get; }
        public int Count { set; get; }
        public Guid Id { set; get; }
        public ICollection<SubCategoryInfoViewModel>? SubCategories { set; get; } 
    }
    public class SubCategoryInfoViewModel
    {
        public required string Name { set; get; }
        public int Count { set; get; }
        public Guid Id { set; get; }
    }

}
