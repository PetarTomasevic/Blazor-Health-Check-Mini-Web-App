using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SystemHealthChecks.Domain.Services.HealthChecks
{
    public class DBHealthCheck : IHealthCheck
    {
        public string ConnectionString { get; }

        public string TestQuery { get; }
        public string Name = "";

        public DBHealthCheck(string connectionString, string testQuery,string databaseName)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            TestQuery = testQuery;
            Name = databaseName;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    if (TestQuery != null)
                    {
                        Stopwatch executionTime = new Stopwatch();

                        executionTime.Start();
                        var command = connection.CreateCommand();
                        command.CommandText = TestQuery;
                        await command.ExecuteNonQueryAsync(cancellationToken);

                        executionTime.Stop();

                        if(executionTime.Elapsed.TotalMilliseconds<0.5)
                        {

                            return HealthCheckResult.Degraded($"Database have slower response then usual in total of {executionTime.Elapsed.TotalSeconds.ToString()} seconds for response");
                        }

                    }
                 
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy();
        }
    }

    public interface IHealthCheckBuilder
    {
        IHealthCheckBuilder Add(Func<IServiceProvider, IHealthCheck> factory);

        IEnumerable<Func<IServiceProvider, IHealthCheck>> GetAll();
    }
}