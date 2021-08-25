using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Common
{
    public interface IAppService<TDto> where TDto : AppDto
    {
        public Task<TDto> Get(int id);
        public Task<IEnumerable<TDto>> GetAll();
        public Task<TDto> Create(TDto entity);
        public Task<TDto> Update(TDto entity);
        public Task<TDto> Delete(int id);
        public Task<IEnumerable<TDto>> Search(string searchString);
    }
}
