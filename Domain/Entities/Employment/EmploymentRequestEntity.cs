using Domain.Entities.Resume;
using Domain.Enum;

namespace Domain.Entities.Employment
{
    public class EmploymentRequestEntity:BaseEntity
    {
        public string? Comment { set; get; }
        public StatusEnum Status { get; set; }   
        public Guid EmploymentId { set; get; }
        public EmploymentEntity? Employment { set; get; }
        public Guid ResumeId { set; get; }
        public ResumeEntity? Resume { set; get; }
 
    }
}
