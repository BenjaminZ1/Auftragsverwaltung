using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private IEnumerable<CustomerDto> _customers;
        private CustomerDto _selectedListItem;
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private Visibility _customerDataGridVisibility;
        private ButtonAction _buttonActionState;

        public IEnumerable<CustomerDto> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(nameof(Customers)); }
        }

        public CustomerDto SelectedListItem
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

        public Visibility CustomerDataGridVisibility
        {
            get => _customerDataGridVisibility;
            set { _customerDataGridVisibility = value; OnPropertyChanged(nameof(CustomerDataGridVisibility)); }
        }

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            DefautlView();
        }

        public static CustomerViewModel LoadCustomerListViewModel(ICustomerService customerService)
        {
            CustomerViewModel customerListViewModel = new CustomerViewModel(customerService);
            customerListViewModel.LoadCustomers();
            return customerListViewModel;
        }

        private void LoadCustomers()
        {
            _customerService.GetAll().ContinueWith(task =>
            {
                if (task.Exception == null)
                    Customers = task.Result;
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
                var serviceTask = await _customerService.Create(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if(serviceTask.Response != null)
                {
                    
                    MessageBox.Show($"Customer with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
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
            var serviceTask = await _customerService.Update(SelectedListItem);
            if (serviceTask.Response != null && !serviceTask.Response.Flag)
            {
                MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (serviceTask.Response != null)
            {
                MessageBox.Show($"Customer with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
                                $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            DefautlView();
        }

        private async Task Delete()
        {
            var serviceTask = await _customerService.Delete(SelectedListItem.CustomerId);
            if (serviceTask.Response != null && !serviceTask.Response.Flag)
            {
                MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (serviceTask.Response != null)
            {
                MessageBox.Show($"Customer with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" + System.Environment.NewLine +
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
            CustomerDataGridVisibility = Visibility.Visible;
            LoadCustomers();
        }

        private void CreateView()
        {
            _buttonActionState = ButtonAction.Create;
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            CustomerDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new CustomerDto();
        }

        private void ModifyView()
        {
            _buttonActionState = ButtonAction.Modify;
            InputEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
            CustomerDataGridVisibility = Visibility.Collapsed;
        }

    }
}
