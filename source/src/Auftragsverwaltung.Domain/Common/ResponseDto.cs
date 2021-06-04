namespace Auftragsverwaltung.Domain.Common
{
    public class ResponseDto<T> where T : EntityBase
    {
        public int Id { get; set; }
        public int NumberOfRows { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
        public T Entity { get; set; }
    }
}
