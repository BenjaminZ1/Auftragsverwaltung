using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Domain.Common;

namespace Auftragsverwaltung.Domain
{
    public class Position : EntityBase
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
