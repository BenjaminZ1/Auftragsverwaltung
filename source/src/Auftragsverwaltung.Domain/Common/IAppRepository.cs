using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Domain.Common
{
    public interface IAppRepository<T> where T : EntityBase
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Create(T entity);
        public Task<T> Update(int id, T entity);
        public Task<bool> Delete(int id);
    }
}
