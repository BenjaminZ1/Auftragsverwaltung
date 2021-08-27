using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CustomerViewModel : CommonViewModel
    {
        private readonly ICustomerService _customerService;
        private IEnumerable<CustomerDto> _customers;
        private CustomerDto _selectedListItem;
        private Visibility _customerPasswordBoxVisibility;

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

        public Visibility CustomerPasswordBoxVisibility
        {
            get => _customerPasswordBoxVisibility;
            set { _customerPasswordBoxVisibility = value; OnPropertyChanged(nameof(CustomerPasswordBoxVisibility)); }
        }

        public CustomerViewModel(ICustomerService customerService) : base()
        {
            _customerService = customerService;
            ControlBarButtonActionCommand = new AsyncCommand(ControlBarButtonAction);
            SearchBoxUpdateCommand = new AsyncCommand(SearchBoxUpdate);
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

        private async Task SearchBoxUpdate(object parameter)
        {
            Customers = await _customerService.Search(SearchText);
        }

        private async Task Save()
        {
            if (ButtonActionState == ButtonAction.Create)
            {
                var serviceTask = await _customerService.Create(SelectedListItem);
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

            if (ButtonActionState == ButtonAction.Modify)
            {
                await Modify();
            }
        }

        private async Task Modify()
        {
            if (SelectedListItem != null)
            {
                var serviceTask = await _customerService.Update(SelectedListItem);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Customer with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
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
                var serviceTask = await _customerService.Delete(SelectedListItem.CustomerId);
                if (serviceTask.Response != null && !serviceTask.Response.Flag)
                {
                    MessageBox.Show(serviceTask.Response.Message, "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else if (serviceTask.Response != null)
                {
                    MessageBox.Show($"Customer with Id: {serviceTask.Response.Id} {serviceTask.Response.Message}" +
                                    System.Environment.NewLine +
                                    $"Affected rows: {serviceTask.Response.NumberOfRows}", "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }

            DefautlView();
        }

        private void DefautlView()
        {
            base.CommonDefautlView();
            CustomerPasswordBoxVisibility = Visibility.Collapsed;
            LoadCustomers();
        }

        private void CreateView()
        {
            base.CommonCreateView();
            CustomerPasswordBoxVisibility = Visibility.Visible;
            SelectedListItem = new CustomerDto();
        }

        private void ModifyView()
        {
            base.CommonModifyView();
            CustomerPasswordBoxVisibility = Visibility.Visible;
        }
    }
}
