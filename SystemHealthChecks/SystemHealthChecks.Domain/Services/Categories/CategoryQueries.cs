using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Domain.Services.Categories
{
    public class CategoryQueries
    {


        //Get List of All Categories
        public async Task<List<HealthCheckCategory>> GetAllCategories()
        {
            try
            {
                using SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext();
                {
                    return await shc.HealthCheckCategory.Include(i => i.HealthCheckSettings).AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            { 
                throw; 
            }
        }

        //Get Category Item
        public async Task<HealthCheckCategory> GetCategory(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    return await shc.HealthCheckCategory.Include(i=>i.HealthCheckSettings).AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
