using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Models;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Auftragsverwaltung.WPF.ViewModels
{
    public class ArticleViewModel : CommonViewModel
    {
        private IEnumerable<ArticleDto> _articles;
        private ArticleDto _selectedListItem;
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

        public ArticleViewModel(IArticleService articleService)
        {
            _articleService = articleService;
            ButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            SearchBoxUpdateCommand = new AsyncCommand(SearchBoxUpdate);
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
            LoadArticles();
        }

        private void CreateView()
        {
            base.CommonCreateView();
            SelectedListItem = new ArticleDto();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
        }
    }
}
