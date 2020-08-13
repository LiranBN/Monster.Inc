using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Monster.Inc.Web.Data;
using Monster.Inc.Web.Models;
using Monster.Inc.Web.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository
{
    public class IntimidiatorRepository : IIntimidiatorRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public IntimidiatorRepository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            logger = loggerFactory.CreateLogger("IntimidiatorRepository");
        }

        public async Task<Intimidiator> CreateIntimidiatorAsync(Intimidiator intimidiator)
        {
            intimidiator.StartedDate = DateTime.Now;
            try
            {
                await context.Set<Intimidiator>().AddAsync(intimidiator);
                await context.SaveChangesAsync();
                return intimidiator;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(CreateIntimidiatorAsync)}: " + ex.Message, intimidiator);
            }

            return null;
        }

        public async Task DeleteIntimidiatorAsync(int id)
        {
            try
            {
                var intimidiator = await context.Intimidiators.FindAsync(id);
                context.Intimidiators.Remove(intimidiator);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(DeleteIntimidiatorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<Intimidiator> GetIntimidiatorAsync(int id)
        {
            try
            {
                return await context.Intimidiators.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(GetIntimidiatorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<Intimidiator> GetIntimidiatorAsync(string userId)
        {
            try
            {
                return await context.Intimidiators.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(GetIntimidiatorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Intimidiator>> GetIntimidiatorsAsync()
        {
            try
            {
                return await context.Intimidiators.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(GetIntimidiatorAsync)}: " + ex.Message);
                throw ex;
            }
        }

        public async Task<Intimidiator> UpdateIntimidiatorAsync(Intimidiator intimidiator)
        {
            try
            {
                var entity = await context.Intimidiators.FindAsync(intimidiator.Id);
                context.Entry(entity).CurrentValues.SetValues(intimidiator);
              await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(UpdateIntimidiatorAsync)}: " + ex.Message, intimidiator);
                throw ex;
            }
            
        }
    }
}
