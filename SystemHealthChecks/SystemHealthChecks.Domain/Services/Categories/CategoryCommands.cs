using System;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Domain.Services.Categories
{
    public class CategoryCommands
    {

        //To Add New Category 
        public  Task<bool> AddHealthCheckCategory(HealthCheckCategory category)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.HealthCheckCategory.Add(category);
                    return Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //To Update Category Item 
        public Task<HealthCheckCategory> UpdateHealthCheckCategory(HealthCheckCategory category)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.Update(category);
                    return Task.FromResult((shc.SaveChanges() > 0 ? category : new HealthCheckCategory()));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //To Delete Category Item
        public Task<bool> DeleteHealthCheckCategory(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    HealthCheckCategory category = shc.HealthCheckCategory.Find(id);
                    shc.HealthCheckCategory.Remove(category);
                    return Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
