using InterviewTask.API.Domain.Enums;

namespace InterviewTask.API.Domain.Entities
{
    public class RoleFeautre : BaseEntity
    {
        public Features Features { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
