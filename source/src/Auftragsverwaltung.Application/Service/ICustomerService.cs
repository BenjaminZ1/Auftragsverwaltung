using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;

namespace Auftragsverwaltung.Application.Service
{
    public interface ICustomerService : IAppService<CustomerDto>
    {
        void Serialize(CustomerDto customer, string filename);
        CustomerDto Deserialize(string filename);
    }
}
