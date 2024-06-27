using Application.Common.Enum;
using Domain.Entities.Resume;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Common.Record.UserPanel
{
    public class MyResumeRecord
    {
        public Guid Id { set; get; }
        public string? Linkdin { set; get; }
        public string? WorkRegistration { set; get; }
        public string? Title { set; get; }
        public Gender? Gender { set; get; }
        public string? Birthday { set; get; }
        public string? MilitaryService { set; get; }
        public string? MaritalStatus { set; get; }
        public string? RightsRequested { set; get; }
        public List<string>? UserSkills { set; get; }
        public Guid UserId { set; get; }
        public string? JsonEducationals { set; get; }
        public string? JsonExperiences { set; get; }
        [BindNever]
        public List<ResumeHistory>? Educationals { set; get; }

        [BindNever]
        public List<ResumeHistory>? Experiences { set; get; }

    }
}
