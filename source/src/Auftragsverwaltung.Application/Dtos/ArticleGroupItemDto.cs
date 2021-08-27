using System.Collections.ObjectModel;

namespace Auftragsverwaltung.Application.Dtos
{
    public class ArticleGroupItemDto
    {
        public string Name { get; set; }

        public ObservableCollection<ArticleGroupItemDto> Items { get; set; }
    }
}
