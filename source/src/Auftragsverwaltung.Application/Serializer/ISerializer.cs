using Auftragsverwaltung.Application.Dtos;

namespace Auftragsverwaltung.Domain.Common
{
    public interface ISerializer<T> where T : AppDto
    {
        void Serialize(T obj, string filename);
        T Deserialize(string filename);
    }
}
