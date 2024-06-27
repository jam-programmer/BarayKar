
using Domain.Entities.System.Identity;
using Domain.Enum;

namespace Domain.Entities.Resume
{
    public class ResumeEntity : BaseEntity
    {
        public string? Linkdin { set; get; }
        public string? WorkRegistration{set;get;}
        public string? Title { set; get; }
        public GenderEnum? Gender { set; get; }
        public string? Birthday { set; get; }
        public string? MilitaryService { set; get; }
        public string? MaritalStatus { set; get; }
        public string? RightsRequested { set; get; }
        public string? Skills { set; get; }
        public Guid UserId { set; get; }
        public UserEntity? User { set; get; }
        public ICollection<EducationalRecordEntity>? Educationals { set; get; }   
        public ICollection<WorkExperienceEntity>? Experiences { set; get; }
    }
}
