namespace Domain.Entities.System.Identity
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public required string PresianName { set; get; }
    }
}
