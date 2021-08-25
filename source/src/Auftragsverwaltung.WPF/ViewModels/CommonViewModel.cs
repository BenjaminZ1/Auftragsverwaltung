using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Auftragsverwaltung.Application.Service;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CommonViewModel : ViewModelBase
    {
        #region Common
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private bool _searchButtonEnabled;
        private ButtonAction _buttonActionState;
        private string _searchText;
        private Visibility _dataGridVisibility;

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

        public bool SearchButtonEnabled
        {
            get => _searchButtonEnabled;
            set { _searchButtonEnabled = value; OnPropertyChanged(nameof(SearchButtonEnabled)); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        public ButtonAction ButtonActionState
        {
            get => _buttonActionState;
            set => _buttonActionState = value;
        }

        public Visibility DataGridVisibility
        {
            get => _dataGridVisibility;
            set { _dataGridVisibility = value; OnPropertyChanged(nameof(DataGridVisibility)); }
        }

        public IAsyncCommand ControlBarButtonActionCommand { get; set; }

        public IAsyncCommand SearchBoxUpdateCommand { get; set; }
        #endregion


        public CommonViewModel() { }

        protected void CommonDefautlView()
        {
            InputEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
            SearchButtonEnabled = true;
            ButtonActionState = ButtonAction.None;
            DataGridVisibility = Visibility.Visible;
        }

        protected void CommonCreateView()
        {
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
            SearchButtonEnabled = false;
            ButtonActionState = ButtonAction.Create;
            DataGridVisibility = Visibility.Collapsed;
        }

        protected void CommonModifyView()
        {
            InputEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
            SearchButtonEnabled = false;
            ButtonActionState = ButtonAction.Modify;
            DataGridVisibility = Visibility.Collapsed;
        }
    }
}
