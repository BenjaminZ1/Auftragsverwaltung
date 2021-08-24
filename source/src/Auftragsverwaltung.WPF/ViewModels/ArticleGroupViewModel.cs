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
    public class ArticleGroupViewModel : CommonViewModel
    {
        private IEnumerable<ArticleGroupDto> _articleGroups;
        private ArticleGroupDto _selectedListItem;
        private Visibility _articleGroupDataGridVisibility;
        private readonly IArticleGroupService _articleGroupService;
        private ButtonAction _buttonActionState;

        public IEnumerable<ArticleGroupDto> ArticleGroups
        {
            get => _articleGroups;
            set { _articleGroups = value; OnPropertyChanged(nameof(ArticleGroups)); }
        }

        public ArticleGroupDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
        }

        public Visibility ArticleGroupDataGridVisibility
        {
            get => _articleGroupDataGridVisibility;
            set { _articleGroupDataGridVisibility = value; OnPropertyChanged(nameof(ArticleGroupDataGridVisibility)); }
        }

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }


        public ArticleGroupViewModel(IArticleGroupService articleGroupService)
        {
            _articleGroupService = articleGroupService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            DefautlView();
        }

        public static ArticleGroupViewModel LoadArticleListViewModel(IArticleGroupService articleGroupService)
        {
            ArticleGroupViewModel articleListviewModel = new ArticleGroupViewModel(articleGroupService);
            articleListviewModel.LoadArticleGroups();
            return articleListviewModel;
        }

        private void LoadArticleGroups()
        {
            _articleGroupService.GetAll().ContinueWith(task =>
            {
                if (task.Exception == null)
                    ArticleGroups = task.Result;
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
                var serviceTask = await _articleGroupService.Create(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    if (serviceTask.Response != null)
                        MessageBox.Show($"ArticleGroup with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
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
            var serviceTask = await _articleGroupService.Update(SelectedListItem);
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
            var serviceTask = await _articleGroupService.Delete(SelectedListItem.ArticleGroupId);
            if (serviceTask.Response != null && !serviceTask.Response.Flag)
            {
                MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (serviceTask.Response != null)
            {
                MessageBox.Show($"ArticleGroup with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DefautlView();
        }

        private void DefautlView()
        {
            _buttonActionState = ButtonAction.None;
            InputEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
            ArticleGroupDataGridVisibility = Visibility.Visible;
            LoadArticleGroups();
        }

        private void CreateView()
        {
            _buttonActionState = ButtonAction.Create;
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            ArticleGroupDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new ArticleGroupDto();
        }

        private void ModifyView()
        {
            _buttonActionState = ButtonAction.Modify;
            InputEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
            ArticleGroupDataGridVisibility = Visibility.Collapsed;
        }
    }   
}
