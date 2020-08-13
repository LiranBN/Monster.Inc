using Monster.Inc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository.Interfaces
{
    public interface IDoorRepository
    {
        Task<Door> GetDoorAsync(int id);
        Task<IEnumerable<Door>> GetDoorsAsync();

        Task<Door> CreateDoorAsync(Door door);

        Task<Door> UpdateDoorAsync(Door door);

        Task DeleteDoorAsync(int id);
    }
}
