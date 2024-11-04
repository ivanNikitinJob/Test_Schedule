using Entities.BaseEntities;

namespace Entities
{
    public class Interval: DeleteAuditedEntity
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
