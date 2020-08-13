using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Monster.Inc.Web.Data;
using Monster.Inc.Web.Infrastructure;
using Monster.Inc.Web.Models;
using Monster.Inc.Web.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository
{
    public class WorkDayRepository : IWorkDayRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IDoorRepository doorRepository;
        private readonly IIntimidiatorRepository intimidiatorRepository;
        private readonly ILogger logger;
        private readonly ICacheProvider _cacheProvider;

        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        public WorkDayRepository(ApplicationDbContext context, 
            ILoggerFactory loggerFactory,
            IDoorRepository doorRepository,
            IIntimidiatorRepository intimidiator,
            ICacheProvider cacheProvider)
        {
            this.context = context;
            this.doorRepository = doorRepository;
            intimidiatorRepository = intimidiator;
            _cacheProvider = cacheProvider;
            logger = loggerFactory.CreateLogger("WorkDayRepository");
        }
        public async Task EndScaringAsync(int IntimidiatorId, int DoorId)
        {
            try
            {
                var entity = await context.Set<WorkDay>()
                    .Include(x=>x.Door)
                    .Include(x=>x.Intimidiator)
                    .FirstAsync(x => x.IntimidiatorId == IntimidiatorId && x.DoorId == DoorId && x.Start.Date == DateTime.Now.Date);
                entity.EndTime = DateTime.Now;
                entity.EnergyCollected = CalculateEnrgyCollected(entity.Intimidiator, entity.Door);
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(EndScaringAsync)}: " + ex.Message);
                throw ex;
            }
        }

        private double? CalculateEnrgyCollected(Intimidiator intimidiator, Door door)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - intimidiator.StartedDate;
            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int years = ( zeroTime + span).Year - 1;

            return Math.Min(100 + years * 20, door.Energy);
        }

        public async Task<IEnumerable<DtoWorkDay>> GetDailyWorkAsync(int IntimidiatorId, DateTime? from, DateTime? To)
        {

            var query = context.WorkDays
                .Include(x => x.Door)
                .Include(x => x.Intimidiator)
                .Where(x => x.IntimidiatorId == IntimidiatorId);

            if (from.HasValue)
            {
                if (To.HasValue)
                {
                    query = query.Where(x => x.Start.Date >= from.Value.Date && x.Start.Date <= To.Value.Date);
                }
                else
                {
                    query = query.Where(x => x.Start.Date == from.Value.Date);
                }
            }

            return await query.Select(x => new DtoWorkDay
            {
                DoorId = x.DoorId,
                DoorName = x.Door.Name,
                EndTime = x.EndTime,
                EnergyCollected = x.EnergyCollected,
                Id = x.Id,
                IntimidiatorId = x.IntimidiatorId,
                IntimidiatorName = $"{x.Intimidiator.FirstName} {x.Intimidiator.LastName}",
                Start = x.Start
            }).ToListAsync();
        }

        public async Task StartScaringAsync(int IntimidiatorId, int DoorId)
        {
            var workDay = new WorkDay
            {
                DoorId = DoorId,
                IntimidiatorId = IntimidiatorId,
                Start = DateTime.Now
            };

            try
            {
                await context.Set<WorkDay>().AddAsync(workDay);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in {nameof(StartScaringAsync)}: " + ex.Message);
                throw ex;
            }

        }

        public Task<string> VerifyWorkDayAsync(int IntimidiatorId, int DoorId, DateTime startWorkDate)
        {
            
        }

        private async Task<IEnumerable<T>> GetCachedResponse<T>(string cacheKey, SemaphoreSlim semaphore, Func<Task<IEnumerable<T>>> func)
        {
            var items = _cacheProvider.GetFromCache<IEnumerable<T>>(cacheKey);

            if (items != null) return items;
            try
            {
                await semaphore.WaitAsync();
                items = _cacheProvider.GetFromCache<IEnumerable<T>>(cacheKey); // Recheck to make sure it didn't populate before entering semaphore
                if (items != null)
                {
                    return items;
                }
                items = await func();
                _cacheProvider.SetCache(cacheKey, items, DateTimeOffset.Now.AddDays(1));
            }
            finally
            {
                semaphore.Release();
            }

            return items;
        }
    }
}
