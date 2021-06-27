using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;


namespace Auftragsverwaltung.WPF.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        private IEnumerable<ArticleDto> _articles;
        private ArticleDto _selectedListItem;
        private bool _textBoxEnabled;
        private bool _saveButtonEnabled;
        private Visibility _articleDataGridVisibility;
        private readonly IArticleService _articleService;

        public IEnumerable<ArticleDto> Articles
        {
            get => _articles;
            set { _articles = value; OnPropertyChanged(nameof(Articles)); }
        }

        public ArticleDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
        }

        public bool TextBoxEnabled
        {
            get => _textBoxEnabled;
            set { _textBoxEnabled = value; OnPropertyChanged(nameof(TextBoxEnabled)); }
        }

        public bool SaveButtonEnabled
        {
            get => _saveButtonEnabled;
            set { _saveButtonEnabled = value; OnPropertyChanged(nameof(SaveButtonEnabled)); }
        }

        public Visibility ArticleDataGridVisibility
        {
            get => _articleDataGridVisibility;
            set { _articleDataGridVisibility = value; OnPropertyChanged(nameof(ArticleDataGridVisibility)); }
        }

        public ICommand ControlBartButtonActionCommand { get; set; }

        public ArticleViewModel(IArticleService articleService)
        {
            _articleService = articleService;
            ControlBartButtonActionCommand = new BaseCommand(ControlBarButtonAction);
            ArticleDataGridVisibility = Visibility.Visible;
        }

        public static ArticleViewModel LoadArticleListViewModel(IArticleService articleService)
        {
            ArticleViewModel articleListviewModel = new ArticleViewModel(articleService);
            articleListviewModel.LoadArticles();
            return articleListviewModel;
        }

        private void LoadArticles()
        {
            _articleService.GetAll().ContinueWith(task =>
            {
                if (task.Exception == null)
                    Articles = task.Result;
            });
        }

        private void ControlBarButtonAction(object parameter)
        {
            if (parameter is ButtonAction)
            {
                ButtonAction buttonAction = (ButtonAction)parameter;
                switch (buttonAction)
                {
                    case ButtonAction.Create:
                        TextBoxEnabled = true;
                        SaveButtonEnabled = true;
                        ArticleDataGridVisibility = Visibility.Hidden;
                        SelectedListItem = null;
                        break;
                    default:
                        throw new ArgumentException("The ButtonAction has no defined action", nameof(ButtonAction));
                }
            }
        }
    }   
}
