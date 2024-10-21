using InterviewTask.API.Domain.Enums;

namespace InterviewTask.API.Domain.Entities
{
    public sealed class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public Features Features { get; set; }
    }
}
