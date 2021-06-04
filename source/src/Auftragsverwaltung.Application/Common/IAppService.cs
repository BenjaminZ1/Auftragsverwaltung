using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Application.Common
{
    public interface IAppService<T> where T : AppDto
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<CustomerDto> Create(T entity);
        public Task<CustomerDto> Update(int id, T entity);
        public Task<CustomerDto> Delete(int id);
    }
}
