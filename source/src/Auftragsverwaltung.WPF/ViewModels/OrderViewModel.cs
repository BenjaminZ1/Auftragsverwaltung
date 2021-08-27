using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Controls;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class OrderViewModel : CommonViewModel
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IArticleService _articleService;
        private IEnumerable<OrderDto> _orders;
        private IEnumerable<CustomerDto> _customers;
        private IEnumerable<ArticleDto> _articles;
        private OrderDto _selectedListItem;
        private ArticleDto _selectedArticleListItem;
        private ObservableCollection<PositionDto> _addedPositionListItems;
        private PositionDto _selectedAddedPositionListItem;
        private int _amount;
        private bool _dateTimePickerEnabled;

        private Visibility _customerPasswordBoxVisibility;

        private UserControl _displayView;

        public IEnumerable<OrderDto> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(nameof(Orders)); }
        }
        public IEnumerable<CustomerDto> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(nameof(Customers)); }
        }
        public IEnumerable<ArticleDto> Articles
        {
            get => _articles;
            set { _articles = value; OnPropertyChanged(nameof(Articles)); }
        }

        public ObservableCollection<PositionDto> AddedPositionListItems
        {
            get => _addedPositionListItems;
            set { _addedPositionListItems = value; OnPropertyChanged(nameof(AddedPositionListItems)); }
        }

        public PositionDto SelectedAddedPositionListItem
        {
            get => _selectedAddedPositionListItem;
            set { _selectedAddedPositionListItem = value; OnPropertyChanged(nameof(SelectedAddedPositionListItem)); }
        }

        public OrderDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
        }

        public ArticleDto SelectedArticleListItem
        {
            get => _selectedArticleListItem;
            set { _selectedArticleListItem = value; OnPropertyChanged(nameof(SelectedArticleListItem)); }
        }

        public int Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        public bool DateTimePickerEnabled
        {
            get => _dateTimePickerEnabled;
            set { _dateTimePickerEnabled = value; OnPropertyChanged(nameof(DateTimePickerEnabled)); }
        }

        public Visibility CustomerPasswordBoxVisibility
        {
            get => _customerPasswordBoxVisibility;
            set { _customerPasswordBoxVisibility = value; OnPropertyChanged(nameof(CustomerPasswordBoxVisibility)); }
        }

        public UserControl DisplayView
        {
            get => _displayView;
            set { _displayView = value; OnPropertyChanged(nameof(DisplayView)); }
        }
        public BaseCommand AddArticleToOrderCommand { get; set; }

        public BaseCommand RemovePositionFromOrderCommand { get; set; }

        public OrderViewModel(IOrderService orderService, ICustomerService customerService, IArticleService articleService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _articleService = articleService;

            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            SearchBoxUpdateCommand = new AsyncCommand(SearchBoxUpdate);

            AddArticleToOrderCommand = new BaseCommand(AddArticleToOrder);
            RemovePositionFromOrderCommand = new BaseCommand(RemovePositionFromOrder);

            _amount = 1;
            CustomerPasswordBoxVisibility = Visibility.Collapsed;
            DefautlView();
        }

        public static OrderViewModel LoadOrderListViewModel(IOrderService orderService, ICustomerService customerService, IArticleService articleService)
        {
            OrderViewModel orderListViewModel = new OrderViewModel(orderService, customerService, articleService);
            orderListViewModel.LoadData();
            return orderListViewModel;
        }

        private void LoadData()
        {
            _orderService.GetAll().ContinueWith(orderTask =>
                {
                    if (orderTask.Exception == null)
                        Orders = orderTask.Result;
                })
                .ContinueWith(customerTask =>
                {
                    _customerService.GetAll().ContinueWith(customerInnerTask =>
                        {
                            if (customerInnerTask.Exception == null)
                                Customers = customerInnerTask.Result;
                        })
                        .ContinueWith(articleTask =>
                        {
                            _articleService.GetAll().ContinueWith(articleInnerTask =>
                            {
                                if (articleInnerTask.Exception == null)
                                    Articles = articleInnerTask.Result;
                            });
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
            Orders = await _orderService.Search(SearchText);
        }

        private async Task Save()
        {
            if (ButtonActionState == ButtonAction.Create)
            {
                var serviceTask = await _orderService.Create(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {

                    MessageBox.Show($"Order with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                    $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }

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
                var serviceTask = await _orderService.Update(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Order with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
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
                var serviceTask = await _orderService.Delete(SelectedListItem.OrderId);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Order with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
                                    System.Environment.NewLine +
                                    $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }

            DefautlView();
        }

        private void AddArticleToOrder(object parameter)
        {
            if (SelectedArticleListItem != null)
            {
                PositionDto positionDto = new PositionDto() { Amount = Amount, Article = SelectedArticleListItem };

                var existingPositionDto = CheckForExistingPositionDto(positionDto);
                if (existingPositionDto != null)
                {
                    RemovePositionDtoFromList(SelectedListItem.Positions, existingPositionDto.Article.ArticleId);
                    RemovePositionDtoFromList(AddedPositionListItems, existingPositionDto.Article.ArticleId);

                    positionDto = existingPositionDto;
                }

                SelectedListItem.Positions.Add(positionDto);
                AddedPositionListItems.Add(positionDto);
            }
        }

        private void RemovePositionFromOrder(object parameter)
        {
            if (SelectedListItem.Positions != null && SelectedAddedPositionListItem != null)
            {
                RemovePositionDtoFromList(SelectedListItem.Positions, SelectedAddedPositionListItem.Article.ArticleId);
                RemovePositionDtoFromList(AddedPositionListItems, SelectedAddedPositionListItem.Article.ArticleId);
            }
        }

        private PositionDto CheckForExistingPositionDto(PositionDto positionDto)
        {
            var existingPositionDto = SelectedListItem.Positions
                .SingleOrDefault(p => p.Article.ArticleId == positionDto.Article.ArticleId);
            if (existingPositionDto != null)
            {
                existingPositionDto.Amount += positionDto.Amount;
                return existingPositionDto;
            }

            return null;
        }

        private void RemovePositionDtoFromList(ICollection<PositionDto> positionDtos, int articleId)
        {
            positionDtos.Remove(positionDtos.SingleOrDefault(p => p.Article.ArticleId == articleId));
        }

        private void DefautlView()
        {
            base.CommonDefautlView();
            DisplayView = new OrderListDetails();
            LoadData();
            DateTimePickerEnabled = false;
        }

        private void CreateView()
        {
            base.CommonCreateView();
            DisplayView = new OrderListModify();
            DateTimePickerEnabled = true;
            SelectedListItem = new OrderDto { Date = DateTime.Now };
            AddedPositionListItems = new ObservableCollection<PositionDto>();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
            DisplayView = new OrderListModify();
            DateTimePickerEnabled = true;
            AddedPositionListItems = new ObservableCollection<PositionDto>();
            if (SelectedListItem != null)
            {
                foreach (var position in SelectedListItem.Positions)
                {
                    AddedPositionListItems.Add(position);
                }
            }
        }
    }
}
