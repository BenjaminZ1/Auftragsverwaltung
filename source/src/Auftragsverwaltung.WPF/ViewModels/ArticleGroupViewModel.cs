using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Auftragsverwaltung.WPF.Models;


namespace Auftragsverwaltung.WPF.ViewModels
{
    public class ArticleGroupViewModel : CommonViewModel
    {
        private readonly IArticleGroupService _articleGroupService;
        private IEnumerable<ArticleGroupDto> _articleGroups;
        private IEnumerable<ArticleGroupDto> _rootArticleGroups;
        private ArticleGroupDto _selectedListItem;

        public IEnumerable<ArticleGroupDto> ArticleGroups
        {
            get => _articleGroups;
            set { _articleGroups = value; OnPropertyChanged(nameof(ArticleGroups)); }
        }

        public IEnumerable<ArticleGroupDto> RootArticleGroups
        {
            get => _rootArticleGroups;
            set { _rootArticleGroups = value; OnPropertyChanged(nameof(RootArticleGroups)); }
        }

        public ArticleGroupDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
        }

        public ArticleGroupViewModel(IArticleGroupService articleGroupService)
        {
            _articleGroupService = articleGroupService;
            ButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            SearchBoxUpdateCommand = new AsyncCommand(SearchBoxUpdate);
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
            })
                .ContinueWith(articleTask =>
                {
                    _articleGroupService.GetHierarchicalData().ContinueWith(articleGroupInnerTask =>
                    {
                        if (articleGroupInnerTask.Exception == null)
                            RootArticleGroups = articleGroupInnerTask.Result;
                    });
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

        private async Task SearchBoxUpdate(object parameter)
        {
            ArticleGroups = await _articleGroupService.Search(SearchText);
        }

        private async Task Save()
        {
            if (ButtonActionState == ButtonAction.Create)
            {
                var serviceTask = await _articleGroupService.Create(SelectedListItem);
                ShowMessageBox(new PlainResponse()
                {
                    Flag = serviceTask.Response.Flag,
                    Id = serviceTask.Response.Id,
                    Message = serviceTask.Response.Message,
                    NumberOfRows = serviceTask.Response.NumberOfRows
                });

                DefautlView();
            }

            if (ButtonActionState == ButtonAction.Modify)
            {
                await Modify();
            }
        }

        private async Task Modify()
        {
            if (SelectedListItem != null)
            {
                var serviceTask = await _articleGroupService.Update(SelectedListItem);
                ShowMessageBox(new PlainResponse()
                {
                    Flag = serviceTask.Response.Flag,
                    Id = serviceTask.Response.Id,
                    Message = serviceTask.Response.Message,
                    NumberOfRows = serviceTask.Response.NumberOfRows
                });
            }
            DefautlView();
        }

        private async Task Delete()
        {
            if (SelectedListItem != null)
            {
                var serviceTask = await _articleGroupService.Delete(SelectedListItem.ArticleGroupId);
                ShowMessageBox(new PlainResponse()
                {
                    Flag = serviceTask.Response.Flag,
                    Id = serviceTask.Response.Id,
                    Message = serviceTask.Response.Message,
                    NumberOfRows = serviceTask.Response.NumberOfRows
                });
            }

            DefautlView();
        }

        private void DefautlView()
        {
            base.CommonDefautlView();
            LoadArticleGroups();
        }

        private void CreateView()
        {
            base.CommonCreateView();
            SelectedListItem = new ArticleGroupDto();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
        }
    }
}
