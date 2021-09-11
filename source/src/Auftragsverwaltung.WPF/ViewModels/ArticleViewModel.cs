using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Models;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Auftragsverwaltung.WPF.Controls;


namespace Auftragsverwaltung.WPF.ViewModels
{
    public class ArticleViewModel : CommonViewModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleGroupService _articleGroupService;
        private IEnumerable<ArticleDto> _articles;
        private IEnumerable<ArticleGroupDto> _articleGroups;
        private ArticleDto _selectedListItem;


        public IEnumerable<ArticleDto> Articles
        {
            get => _articles;
            set { _articles = value; OnPropertyChanged(nameof(Articles)); }
        }

        public IEnumerable<ArticleGroupDto> ArticleGroups
        {
            get => _articleGroups;
            set { _articleGroups = value; OnPropertyChanged(nameof(ArticleGroups)); }
        }

        public ArticleDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
        }

        public ArticleViewModel(IArticleService articleService, IArticleGroupService articleGroupService)
        {
            _articleService = articleService;
            _articleGroupService = articleGroupService;
            ButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            SearchBoxUpdateCommand = new AsyncCommand(SearchBoxUpdate);
            DefautlView();
        }

        public static ArticleViewModel LoadArticleListViewModel(IArticleService articleService, IArticleGroupService articleGroupService)
        {
            ArticleViewModel articleListviewModel = new ArticleViewModel(articleService, articleGroupService);
            articleListviewModel.LoadArticles();
            return articleListviewModel;
        }

        private void LoadArticles()
        {
            _articleService.GetAll().ContinueWith(task =>
                {
                    if (task.Exception == null)
                        Articles = task.Result;
                })
                .ContinueWith(articleGroupTask =>
                {
                    _articleGroupService.GetAll().ContinueWith(articleGroupInnerTask =>
                    {
                        if (articleGroupInnerTask.Exception == null)
                            ArticleGroups = articleGroupInnerTask.Result;
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
            Articles = await _articleService.Search(SearchText);
        }

        private async Task Save()
        {
            if (ButtonActionState == ButtonAction.Create)
            {
                var serviceTask = await _articleService.Create(SelectedListItem);
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
                var serviceTask = await _articleService.Update(SelectedListItem);
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
                var serviceTask = await _articleService.Delete(SelectedListItem.ArticleId);
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
            DisplayView = new ArticleListDetails();
            LoadArticles();
        }

        private void CreateView()
        {
            base.CommonCreateView();
            DisplayView = new ArticleListModify();
            SelectedListItem = new ArticleDto();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
            DisplayView = new ArticleListModify();
        }
    }
}
