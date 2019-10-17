using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemHealthChecks.Infrastructure.Entities
{
    public class HealthCheckCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HealthCheckCategoryName { get; set; }
        public string HealthCheckCategoryDescription { get; set; }

        public virtual ICollection<HealthCheckSettings> HealthCheckSettings { get; set; }
    }
}
