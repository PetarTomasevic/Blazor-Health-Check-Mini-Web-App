using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;


namespace SystemHealthChecks.Domain.Services.ApiHealthCheck
{
    public class UrlApiHealthCheckCommands
    {
        private readonly static Serilog.ILogger _logger = Log.ForContext(typeof(UrlApiHealthCheckCommands));
        public UrlApiHealthCheckCommands()
        {
        }
        //To Add New Category 
        public async Task<bool> NewUrlApiHealthCheck(UrlApiHealthCheck urlApibHealthCheck)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.UrlApiHealthCheck.Add(urlApibHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //To Update Category Item 
        public async Task<UrlApiHealthCheck> UpdateUrlApiHealthCheck(UrlApiHealthCheck urlApibHealthCheck)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.Update(urlApibHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? urlApibHealthCheck : new UrlApiHealthCheck()));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //To Delete Category Item
        public async Task<bool> DeleteUrlApiHealthCheck(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    _logger.Information($"we called {nameof(DeleteUrlApiHealthCheck)} with {nameof(id)}: {id}");
                    UrlApiHealthCheck urlApibHealthCheck = shc.UrlApiHealthCheck.Find(id);
                    shc.UrlApiHealthCheck.Remove(urlApibHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(nameof(DeleteUrlApiHealthCheck), ex.ToString());
                return false;


            }
        }

    }
}
