using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemHealthChecks.Infrastructure.Entities
{
    public class UrlApiHealthCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HostUrl { get; set; }
        public string TestApiPath { get; set; }
        public string Description { get; set; }
    }
}
