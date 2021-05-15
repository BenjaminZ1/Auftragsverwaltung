using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    public class Article
    {
#nullable enable
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual ArticleGroup? ArticleGroup { get; set; }
#nullable disable
    }
}
