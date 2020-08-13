using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Monster.Inc.Web.Data;
using Monster.Inc.Web.Models;
using Monster.Inc.Web.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository
{
    public class DoorRepository : IDoorRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public DoorRepository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            logger = loggerFactory.CreateLogger("DoorRepository");
        }
        public async Task<Door> CreateDoorAsync(Door door)
        {
            try
            {
                await context.Doors.AddAsync(door);
                await context.SaveChangesAsync();
                return door;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(CreateDoorAsync)}: " + ex.Message, door);
                throw ex;
            }
        }

        public async Task DeleteDoorAsync(int id)
        {
            try
            {
                var item = await context.Set<Door>().FindAsync(id);
                context.Remove(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(DeleteDoorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<Door> GetDoorAsync(int id)
        {
            try
            {
                return await context.Set<Door>().FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(GetDoorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Door>> GetDoorsAsync()
        {
            try
            {
                return await context.Doors.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(GetDoorsAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<Door> UpdateDoorAsync(Door door)
        {
            try
            {
                var entity = await context.Set<Door>().FindAsync(door.Id);
                context.Entry(entity).CurrentValues.SetValues(door);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(UpdateDoorAsync)}: " + ex.Message, door);
                throw ex;
            }
        }
    }
}
