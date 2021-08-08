using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Position;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Controls;
using Auftragsverwaltung.WPF.State;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class OrderViewModel : ViewModelBase
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
        private int _amount;
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private Visibility _orderDataGridVisibility;
        private ButtonAction _buttonActionState;

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

        public bool InputEnabled
        {
            get => _inputEnabled;
            set { _inputEnabled = value; OnPropertyChanged(nameof(InputEnabled)); }
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

        public Visibility OrderDataGridVisibility
        {
            get => _orderDataGridVisibility;
            set { _orderDataGridVisibility = value; OnPropertyChanged(nameof(OrderDataGridVisibility)); }
        }

        public UserControl DisplayView
        {
            get => _displayView;
            set { _displayView = value; OnPropertyChanged(nameof(DisplayView)); }
        }

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }

        public BaseCommand AddArticleToOrderCommand { get; set; }

        public OrderViewModel(IOrderService orderService, ICustomerService customerService, IArticleService articleService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _articleService = articleService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            AddArticleToOrderCommand = new BaseCommand(AddArticleToOrder);
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
        private async Task Save()
        {
            if (_buttonActionState == ButtonAction.Create)
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

            if (_buttonActionState == ButtonAction.Modify)
            {
                await Modify();
            }
        }

        private async Task Modify()
        {
            var serviceTask = await _orderService.Update(SelectedListItem);
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

        private async Task Delete()
        {
            var serviceTask = await _orderService.Delete(SelectedListItem.OrderId);
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

        private void AddArticleToOrder(object parameter)
        {
            PositionDto positionDto = new PositionDto() {Amount = Amount, Article = SelectedArticleListItem};
            SelectedListItem.Positions.Add(positionDto);
            AddedPositionListItems.Add(positionDto);
        }

        private void DefautlView()
        {
            DisplayView = new OrderListDetails();
            LoadData();
            _buttonActionState = ButtonAction.None;
            InputEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
            OrderDataGridVisibility = Visibility.Visible;
        }

        private void CreateView()
        {
            DisplayView = new OrderListModify();
            _buttonActionState = ButtonAction.Create;
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            OrderDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new OrderDto();
            AddedPositionListItems = new ObservableCollection<PositionDto>();
        }

        private void ModifyView()
        {
            _buttonActionState = ButtonAction.Modify;
            InputEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
            OrderDataGridVisibility = Visibility.Collapsed;
        }
    }
}
