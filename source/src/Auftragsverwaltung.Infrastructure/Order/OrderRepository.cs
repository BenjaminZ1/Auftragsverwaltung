using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Infrastructure.Order
{
    class OrderRepository : IAppRepository<Domain.Order>
    {

        private readonly AppDbContext _db;

        public Task<Domain.Order> Create(Domain.Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Order> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Order> Update(int id, Domain.Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
