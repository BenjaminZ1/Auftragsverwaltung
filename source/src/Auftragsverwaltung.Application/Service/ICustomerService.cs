using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;

namespace Auftragsverwaltung.Application.Service
{
    public interface ICustomerService : IAppService<CustomerDto>
    {
        Task Serialize(CustomerDto customer, string filename);
        Task<CustomerDto> Deserialize(string filename);
    }
}
