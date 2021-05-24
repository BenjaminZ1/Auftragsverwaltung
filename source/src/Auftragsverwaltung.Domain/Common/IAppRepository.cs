using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;

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
