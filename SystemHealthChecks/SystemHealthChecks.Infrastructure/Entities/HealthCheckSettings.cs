using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SystemHealthChecks.Infrastructure.Entities
{
    public class HealthCheckSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HealthCheckSettingName { get; set; }
        public string HealthCheckSettingDescription { get; set; }
        public string HealthCheckSettingValue { get; set; }

        public virtual HealthCheckCategory HealthCheckCategory { get; set; }
    }
}
