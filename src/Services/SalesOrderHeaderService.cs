using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Services
{
    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _salesOrderHeaderRepository;

        public SalesOrderHeaderService(ISalesOrderHeaderRepository salesOrderHeaderRepository)
        {
            _salesOrderHeaderRepository = salesOrderHeaderRepository;
        }

        public async Task<Tuple<int, IEnumerable<SalesOrderHeader>>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _salesOrderHeaderRepository.GetAllAsync(pageIndex, pageSize);
        }

        public async Task<SalesOrderHeader> GetByIdAsync(int id)
        {
            return await _salesOrderHeaderRepository.GetByIdAsync(id);
        }

        public void BulkInsert(IEnumerable<SalesOrderHeader> entityList)
        {
            _salesOrderHeaderRepository.BulkInsert(entityList);
        }

        public void BulkUpdate(IEnumerable<SalesOrderHeader> entityList)
        {
            _salesOrderHeaderRepository.BulkUpdate(entityList);
        }

        public void BulkDelete(IEnumerable<SalesOrderHeader> entityList)
        {
            _salesOrderHeaderRepository.BulkDelete(entityList);
        }
    }
}