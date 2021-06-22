using System;
using System.Collections.Generic;
using System.Text;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CustomerListViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private IEnumerable<CustomerDto> _customers;
        private CustomerDto _selectedListItem;
        private bool _textBoxEnabled;

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

        public CustomerListViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public static CustomerListViewModel LoadCustomerListViewModel(ICustomerService customerService)
        {
            CustomerListViewModel customerListViewModel = new CustomerListViewModel(customerService);
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
    }
}
