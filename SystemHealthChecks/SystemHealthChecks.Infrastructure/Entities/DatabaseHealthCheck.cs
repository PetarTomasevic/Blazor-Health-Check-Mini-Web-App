using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemHealthChecks.Infrastructure.Entities
{
    public class DatabaseHealthCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseConnectionString{ get; set; }
        public string DatabaseTestQuery { get; set; }
    }
}
