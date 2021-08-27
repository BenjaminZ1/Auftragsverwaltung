using System.Collections.Generic;

namespace Auftragsverwaltung.WPF.Models
{
    public class ArticleGroupItem
    {
        public string Name { get; set; }
        public ICollection<ArticleGroupItem> ArticleGroupItems { get; set; }

        public ArticleGroupItem()
        {
            ArticleGroupItems = new List<ArticleGroupItem>();
        }
    }
}
