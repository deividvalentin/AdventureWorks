using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
 
namespace Domain.Services
{
    public interface ISalesOrderHeaderService
    {
        Task<Tuple<int, IEnumerable<SalesOrderHeader>>> GetAllAsync(int pageIndex, int pageSize);
        Task<SalesOrderHeader> GetByIdAsync(int id);
        void BulkInsert(IEnumerable<SalesOrderHeader> entityList);
        void BulkUpdate(IEnumerable<SalesOrderHeader> entityList);
        void BulkDelete(IEnumerable<SalesOrderHeader> entityList);
    }
}