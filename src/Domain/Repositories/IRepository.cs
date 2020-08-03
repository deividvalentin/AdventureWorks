using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        void BulkInsert(IEnumerable<T> entityList);
        void BulkUpdate(IEnumerable<T> entityList);
        void BulkDelete(IEnumerable<T> entityList);
    }
}