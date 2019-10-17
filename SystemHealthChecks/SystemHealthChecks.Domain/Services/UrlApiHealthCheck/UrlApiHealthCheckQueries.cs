using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Domain.Services.ApiHealthCheck
{
    public class UrlApiHealthCheckQueries
    {


        //Get List of All UrlApis
        public async Task<List<UrlApiHealthCheck>> GetListUrlApiHealthCheck()
        {
            try
            {
                using SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext();
                {
                    return await shc.UrlApiHealthCheck.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            { 
                throw; 
            }
        }

        //Get UrlApi Item
        public async Task<Infrastructure.Entities.UrlApiHealthCheck> GetUrlApiHealthCheck(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    return await shc.UrlApiHealthCheck.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
