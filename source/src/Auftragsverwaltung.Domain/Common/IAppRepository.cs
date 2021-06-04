using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Domain.Common
{
    public interface IAppRepository<T> where T : EntityBase
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<ResponseDto<T>> Create(T entity);
        public Task<ResponseDto<T>> Update(int id, T entity);
        public Task<ResponseDto<T>> Delete(int id);
    }
}
