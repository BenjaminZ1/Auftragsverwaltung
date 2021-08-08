using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderService _orderService;
        private IEnumerable<OrderDto> _orders;
        private OrderDto _selectedListItem;
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private Visibility _orderDataGridVisibility;
        private ButtonAction _buttonActionState;

        public IEnumerable<OrderDto> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(nameof(Orders)); }
        }

        public OrderDto SelectedListItem
        {
            get => _selectedListItem;
            set { _selectedListItem = value; OnPropertyChanged(nameof(SelectedListItem)); }
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

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }

        public OrderViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            DefautlView();
        }

        public static OrderViewModel LoadOrderListViewModel(IOrderService orderService)
        {
            OrderViewModel orderListViewModel = new OrderViewModel(orderService);
            orderListViewModel.LoadOrders();
            return orderListViewModel;
        }

        private void LoadOrders()
        {
            _orderService.GetAll().ContinueWith(task =>
            {
                if (task.Exception == null)
                    Orders = task.Result;
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

        private void DefautlView()
        {
            _buttonActionState = ButtonAction.None;
            InputEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
            OrderDataGridVisibility = Visibility.Visible;
            LoadOrders();
        }

        private void CreateView()
        {
            _buttonActionState = ButtonAction.Create;
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            OrderDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new OrderDto();
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
