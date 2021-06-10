using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.WPF.ViewModels;
using Auftragsverwaltung.WPF.Views;

namespace Auftragsverwaltung.WPF.Commands
{
    public class GetCustomerCommand : AsyncCommandBase
    {
        private CustomerViewModel _customerViewModel;

        private ICustomerService _customerService;

        public GetCustomerCommand(CustomerViewModel customerViewModel, ICustomerService customerService)
        {
            _customerViewModel = customerViewModel;
            _customerService = customerService;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
            //await _customerService.Get(2);
        }

        public event EventHandler CanExecuteChanged;
    }
}
