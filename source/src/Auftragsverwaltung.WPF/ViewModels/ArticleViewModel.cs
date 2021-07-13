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
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private Visibility _articleDataGridVisibility;
        private readonly IArticleService _articleService;
        private ButtonAction _buttonActionState;

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

        public bool CreateButtonEnabled
        {
            get => _createButtonEnabled;
            set { _createButtonEnabled = value; OnPropertyChanged(nameof(CreateButtonEnabled)); }
        }

        public bool ModifyButtonEnabled
        {
            get => _modifyButtonEnabled;
            set { _modifyButtonEnabled = value; OnPropertyChanged(nameof(ModifyButtonEnabled)); }
        }

        public bool DeleteButtonEnabled
        {
            get => _deleteButtonEnabled;
            set { _deleteButtonEnabled = value; OnPropertyChanged(nameof(DeleteButtonEnabled)); }
        }

        public Visibility ArticleDataGridVisibility
        {
            get => _articleDataGridVisibility;
            set { _articleDataGridVisibility = value; OnPropertyChanged(nameof(ArticleDataGridVisibility)); }
        }

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }


        public ArticleViewModel(IArticleService articleService)
        {
            _articleService = articleService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            DefautlView();
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

        private async Task ControlBarButtonAction(object parameter)
        {
            var action = parameter as ButtonAction?;
            if (action != null)
            {
                ButtonAction buttonAction = action.Value;
                switch (buttonAction)
                {
                    case ButtonAction.Create:
                        CreateView();
                        break;
                    case ButtonAction.Modify:
                        ModifyView();
                        break;
                    case ButtonAction.Delete:
                        await Delete();
                        break;
                    case ButtonAction.Save:
                        await Save();
                        break;
                    default:
                        throw new ArgumentException("The ButtonAction has no defined action", nameof(parameter));
                }
            }
        }

        private async Task Save()
        {
            if (_buttonActionState == ButtonAction.Create)
            {
                var serviceTask = await _articleService.Create(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    if (serviceTask.Response != null)
                        MessageBox.Show($"Article with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                        $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                }

                DefautlView();
            }

            if (_buttonActionState == ButtonAction.Modify)
            {
                await Modify();
            }
        }

        private async Task Modify()
        {
            var serviceTask = await _articleService.Update(SelectedListItem);
            if (serviceTask.Response != null && !serviceTask.Response.Flag)
            {
                MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (serviceTask.Response != null)
            {
                MessageBox.Show($"Article with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DefautlView();
        }

        private async Task Delete()
        {
            var serviceTask = await _articleService.Delete(SelectedListItem.ArticleId);
            if (serviceTask.Response != null && !serviceTask.Response.Flag)
            {
                MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (serviceTask.Response != null)
            {
                MessageBox.Show($"Article with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DefautlView();
        }

        private void DefautlView()
        {
            _buttonActionState = ButtonAction.None;
            TextBoxEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
            ArticleDataGridVisibility = Visibility.Visible;
            LoadArticles();
        }

        private void CreateView()
        {
            _buttonActionState = ButtonAction.Create;
            TextBoxEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            ArticleDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new ArticleDto();
        }

        private void ModifyView()
        {
            _buttonActionState = ButtonAction.Modify;
            TextBoxEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
            ArticleDataGridVisibility = Visibility.Collapsed;
        }
    }   
}
