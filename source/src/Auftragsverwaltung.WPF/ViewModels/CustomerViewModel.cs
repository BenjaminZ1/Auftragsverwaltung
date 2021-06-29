using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
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

        private void ControlBarButtonAction(object parameter)
        {
            if (parameter is ButtonAction)
            {
                ButtonAction buttonAction = (ButtonAction) parameter;
                switch (buttonAction)
                {
                    case ButtonAction.Create:
                        TextBoxEnabled = true;
                        SaveButtonEnabled = true;
                        CustomerDataGridVisibility = Visibility.Collapsed;
                        SelectedListItem = null;
                        break;
                    default:
                        throw new ArgumentException("The ButtonAction has no definied action", nameof(ButtonAction));
                }
            }
        }
    }
}
