using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Linq;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Infrastructure
{
    public class SystemHealthChecksDbContext : IdentityDbContext
    {

        public DbSet<HealthCheckCategory> HealthCheckCategory { get; set; }
        public DbSet<HealthCheckSettings> HealthCheckSettings { get; set; }
        public DbSet<DatabaseHealthCheck> DatabaseHealthCheck { get; set; }
        public DbSet<UrlApiHealthCheck> UrlApiHealthCheck { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlCon = "Server=(localdb)\\mssqllocaldb;Database=SystemHealthChecks;Trusted_Connection=True;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(sqlCon);
            }

        }
        public SystemHealthChecksDbContext(DbContextOptions<SystemHealthChecksDbContext> options, IHttpContextAccessor httpContextAccessor)
           : base(ChangeOptionsType<SystemHealthChecksDbContext>(options))
        {
        }

        public SystemHealthChecksDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var cascadeFKs = builder.Model.GetEntityTypes()
         .SelectMany(t => t.GetForeignKeys())
         .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(builder);

        }

        protected static DbContextOptions<T> ChangeOptionsType<T>(DbContextOptions options) where T : DbContext
        {
            var sqlExt = options.Extensions.FirstOrDefault(e => e is SqlServerOptionsExtension);

            if (sqlExt == null)
                throw (new Exception("Failed to retrieve SQL connection string for base Context"));

            return new DbContextOptionsBuilder<T>()
                        .UseSqlServer(((SqlServerOptionsExtension)sqlExt).ConnectionString)
                        .Options;
        }
    }
}