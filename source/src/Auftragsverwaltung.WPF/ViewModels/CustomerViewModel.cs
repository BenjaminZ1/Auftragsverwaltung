using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private IEnumerable<CustomerDto> _customers;
        private CustomerDto _selectedListItem;
        private bool _textBoxEnabled;
        private bool _saveButtonEnabled;
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

        public Visibility CustomerDataGridVisibility
        {
            get => _customerDataGridVisibility;
            set { _customerDataGridVisibility = value; OnPropertyChanged(nameof(CustomerDataGridVisibility)); }
        }


        public ICommand ControlBarButtonActionCommand { get; set; }

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            ControlBarButtonActionCommand = new BaseCommand(ControlBarButtonAction);
            CustomerDataGridVisibility = Visibility.Visible;
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

        private async void ControlBarButtonAction(object parameter)
        {
            if (parameter is ButtonAction)
            {
                ButtonAction buttonAction = (ButtonAction) parameter;
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
                        throw new ArgumentException("The ButtonAction has no definied action", nameof(ButtonAction));
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
                else
                {
                    if (serviceTask.Response != null)
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
            else
            {
                if (serviceTask.Response != null)
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
            else
            {
                if (serviceTask.Response != null)
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
            TextBoxEnabled = false;
            SaveButtonEnabled = false;
            CustomerDataGridVisibility = Visibility.Visible;
            LoadCustomers();
        }

        private void CreateView()
        {
            _buttonActionState = ButtonAction.Create;
            TextBoxEnabled = true;
            SaveButtonEnabled = true;
            CustomerDataGridVisibility = Visibility.Collapsed;
            SelectedListItem = new CustomerDto();
        }

        private void ModifyView()
        {
            _buttonActionState = ButtonAction.Modify;
            TextBoxEnabled = true;
            SaveButtonEnabled = true;
            CustomerDataGridVisibility = Visibility.Collapsed;
        }
    }
}
