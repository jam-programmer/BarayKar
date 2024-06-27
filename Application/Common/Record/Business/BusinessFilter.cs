namespace Application.Common.Record.Business
{
    public record BusinessFilter
    {
        public int page { set; get; } = 1;
        public string? search { set; get; }
        public List<Guid>? category { set; get; } = new();
        public List<Guid>? province { set; get; } = new();
        public List<Guid>? city { set; get; } = new();
    }


}
