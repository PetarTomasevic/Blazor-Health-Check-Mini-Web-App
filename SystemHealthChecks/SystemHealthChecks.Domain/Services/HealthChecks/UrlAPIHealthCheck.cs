using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Data.Common;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SystemHealthChecks.Domain.Services.HealthChecks
{
    public class APIHealthCheck : IHealthCheck
    {
        public string HostUrl { get; }
        public string TestApiPath { get; }
        public APIHealthCheck(string hostUrl, string testApiPath)
        {
            HostUrl = hostUrl ?? throw new ArgumentNullException(nameof(hostUrl));
            TestApiPath = testApiPath.Replace("//", "/");
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(HostUrl + TestApiPath);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Url not responding with 200 OK");
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
}