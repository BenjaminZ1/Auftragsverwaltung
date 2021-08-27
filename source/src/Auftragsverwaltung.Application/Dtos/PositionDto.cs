namespace Auftragsverwaltung.Application.Dtos
{
    public class PositionDto : AppDto
    {
        public int OrderId { get; set; }
        public int ArticleId { get; set; }
        public int Amount { get; set; }
        public OrderDto Order { get; set; }
        public ArticleDto Article { get; set; }
    }
}
