using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    public class Position
    {
#nullable enable
        public int OrderId { get; set; }
        public int ArticleId { get; set; }
        public int Amount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Article Article { get; set; }
#nullable disable
    }
}
