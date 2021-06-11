using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.WPF.Commands;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        public CustomerListViewModel CustomerListViewModel { get; set; }

        public CustomerViewModel(CustomerListViewModel customerListViewModel)
        {
            CustomerListViewModel = customerListViewModel;
        }
    }
}
