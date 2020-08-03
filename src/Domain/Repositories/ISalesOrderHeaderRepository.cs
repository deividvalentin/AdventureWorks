using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ISalesOrderHeaderRepository : IRepository<SalesOrderHeader>
    {
        Task<Tuple<int, IEnumerable<SalesOrderHeader>>> GetAllAsync(int pageIndex, int pageSize);
    }
}