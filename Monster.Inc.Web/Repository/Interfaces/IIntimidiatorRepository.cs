using Monster.Inc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Repository.Interfaces
{
    public interface IIntimidiatorRepository
    {
        Task<Intimidiator> GetIntimidiatorAsync(int id);
        Task<Intimidiator> GetIntimidiatorAsync(string userId);

        Task<IEnumerable<Intimidiator>> GetIntimidiatorsAsync();

        Task<Intimidiator> CreateIntimidiatorAsync(Intimidiator intimidiator);

        Task<Intimidiator> UpdateIntimidiatorAsync(Intimidiator intimidiator);

        Task DeleteIntimidiatorAsync(int id);
    }
}
