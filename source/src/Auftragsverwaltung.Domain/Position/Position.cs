using System.ComponentModel.DataAnnotations.Schema;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain.Position
{
    public class Position : EntityBase
    {
#nullable enable
        public int OrderId { get; set; }
        public int ArticleId { get; set; }
        public int Amount { get; set; }
        public virtual Order.Order Order { get; set; } = default!;
        public virtual Article.Article Article { get; set; } = default!;
#nullable disable
    }
}
