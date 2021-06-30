using System.Collections.Generic;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;

namespace Auftragsverwaltung.Application.Common
{
    public interface IAppService<TDto,TEntity> where TDto : AppDto where TEntity : EntityBase
    {
        public Task<TDto> Get(int id);
        public Task<IEnumerable<TDto>> GetAll();
        public Task<TDto> Create(TEntity entity);
        public Task<TDto> Update(TEntity entity);
        public Task<TDto> Delete(int id);
        public TEntity ConvertToEntity(TDto Dto);
    }
}
