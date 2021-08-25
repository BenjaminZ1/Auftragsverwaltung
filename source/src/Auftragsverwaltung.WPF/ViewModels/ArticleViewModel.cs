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
    public class ArticleViewModel : CommonViewModel
    {
        private IEnumerable<ArticleDto> _articles;
        private ArticleDto _selectedListItem;
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

        public Visibility ArticleDataGridVisibility
        {
            get => _articleDataGridVisibility;
            set { _articleDataGridVisibility = value; OnPropertyChanged(nameof(ArticleDataGridVisibility)); }
        }

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
            if (SelectedListItem != null)
            {
                var serviceTask = await _articleService.Update(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Article with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
                                    System.Environment.NewLine +
                                    $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            
            DefautlView();
        }

        private async Task Delete()
        {
            if (SelectedListItem != null)
            {
                var serviceTask = await _articleService.Delete(SelectedListItem.ArticleId);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Article with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
                                    System.Environment.NewLine +
                                    $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }

            DefautlView();
        }

        private void DefautlView()
        {
            base.CommonDefautlView();
            _buttonActionState = ButtonAction.None;
            ArticleDataGridVisibility = Visibility.Visible;
            LoadArticles();
        }

        private void CreateView()
        {
            base.CommonCreateView();
            _buttonActionState = ButtonAction.Create;
            ArticleDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new ArticleDto();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
            _buttonActionState = ButtonAction.Modify;
            ArticleDataGridVisibility = Visibility.Collapsed;
        }
    }   
}
