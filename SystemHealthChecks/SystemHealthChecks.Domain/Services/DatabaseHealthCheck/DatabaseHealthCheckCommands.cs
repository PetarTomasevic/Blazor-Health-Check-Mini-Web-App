using System;
using System.Threading.Tasks;
using SystemHealthChecks.Infrastructure;
using SystemHealthChecks.Infrastructure.Entities;

namespace SystemHealthChecks.Domain.Services.DatabaseHealthChecks
{
    public class DatabaseHealthCheckCommands
    {

        //To Add New Category 
        public  async Task<bool> DBHealthCheck(DatabaseHealthCheck dbHealthCheck)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.DatabaseHealthCheck.Add(dbHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //To Update Category Item 
        public async Task<DatabaseHealthCheck> UpdateHealthCheckCategory(DatabaseHealthCheck dbHealthCheck)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    shc.Update(dbHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? dbHealthCheck : new DatabaseHealthCheck()));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //To Delete Category Item
        public async  Task<bool> DeleteDatabaseHealthCheck(int id)
        {
            try
            {
                using (SystemHealthChecksDbContext shc = new SystemHealthChecksDbContext())
                {
                    DatabaseHealthCheck dbHealthCheck = shc.DatabaseHealthCheck.Find(id);
                    shc.DatabaseHealthCheck.Remove(dbHealthCheck);
                    return await Task.FromResult((shc.SaveChanges() > 0 ? true : false));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
