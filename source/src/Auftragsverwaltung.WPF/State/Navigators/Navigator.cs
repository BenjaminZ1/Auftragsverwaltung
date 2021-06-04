﻿using System.Windows.Input;
using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.Models;
using Auftragsverwaltung.WPF.ViewModels;

namespace Auftragsverwaltung.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
