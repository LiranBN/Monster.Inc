using Monster.Inc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository.Interfaces
{
    public interface IWorkDayRepository
    {
        Task<string> VerifyWorkDayAsync(int IntimidiatorId, int DoorId, DateTime startWorkDate);

        Task StartScaringAsync(int IntimidiatorId, int DoorId);
        Task EndScaringAsync(int IntimidiatorId, int DoorId);

        Task<IEnumerable<DtoWorkDay>> GetDailyWorkAsync(int IntimidiatorId, DateTime? from, DateTime? To);
    }
}
