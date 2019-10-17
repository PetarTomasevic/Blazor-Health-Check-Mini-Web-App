using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Domain.Services.DatabaseHealthChecks
{
    public class DatabaseHealthCheckQueries
    {

        //Get List of All Categories
        public async Task<List<DatabaseHealthCheck>> GetListDatabaseHealthChecks()
        {
            try
            {
                using SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext();
                {
                    return await shc.DatabaseHealthCheck.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        //Get Category Item
        public async Task<DatabaseHealthCheck> GetDatabaseHealthCheck(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    return await shc.DatabaseHealthCheck.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
