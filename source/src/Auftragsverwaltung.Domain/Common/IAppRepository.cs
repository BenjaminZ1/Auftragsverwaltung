using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Domain.Common
{
    public interface IAppRepository<TEntity> where TEntity : EntityBase
    {
        public Task<TEntity> Get(int id);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<ResponseDto<TEntity>> Create(TEntity entity);
        public Task<ResponseDto<TEntity>> Update(TEntity entity);
        public Task<ResponseDto<TEntity>> Delete(int id);
        public Task<IEnumerable<TEntity>> Search(string searchString);
    }
}
