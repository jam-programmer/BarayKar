namespace Domain.Entities.Resume
{
    public class EducationalRecordEntity:BaseEntity
    {
        public string? Title { set; get; }
        public string? Description { set; get; }
        public string? FromDate { set; get; }
        public string? ToDate { set; get; }
        public bool IsStudying { set; get; }

        public Guid ResumeId { set; get; }
        public ResumeEntity? Resume { set; get; }
    }
}
