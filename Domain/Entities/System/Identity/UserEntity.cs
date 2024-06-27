using Domain.Entities.Business;
using Domain.Entities.Employment;
using Domain.Entities.Resume;

namespace Domain.Entities.System.Identity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public required string Avatar { set; get; }
        public ResumeEntity Resume { set; get; }    
        public ICollection<BusinessEntity>? Businesses { get; set; }
        public ICollection<EmploymentEntity>? Employments { set; get; }
    }
}
