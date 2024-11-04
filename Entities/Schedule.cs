using Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Schedule: DeleteAuditedEntity
    {
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
