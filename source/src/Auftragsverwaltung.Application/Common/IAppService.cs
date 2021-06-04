using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Common
{
    public interface IAppService<T1,T2> where T1 : AppDto where T2 : EntityBase
    {
        public Task<T1> Get(int id);
        public Task<IEnumerable<T1>> GetAll();
        public Task<T1> Create(T2 entity);
        public Task<T1> Update(int id, T2 entity);
        public Task<CustomerDto> Delete(int id);
    }
}
