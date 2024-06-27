namespace Application.Common.Record.Employment
{
    public record EmploymentFilter
    {
        public int page { set; get; } = 1;
        public string? search { set; get; }
        public List<Guid>? business { set; get; } = new();
        public List<Guid>? province { set; get; } = new();
        public List<Guid>? city { set; get; } = new();
    }
}
