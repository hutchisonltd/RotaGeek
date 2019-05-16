using RotaGeek.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotaGeek.Core.Interfaces
{
    public interface IRepository
    {
        Task<List<T>> List<T>(CancellationToken ct = default(CancellationToken)) where T : BaseEntity;
        Task<T> Get<T>(int id, CancellationToken ct = default(CancellationToken)) where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
    }
}
