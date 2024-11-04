using Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Group: DeleteAuditedEntity
    {
        public string Name { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
