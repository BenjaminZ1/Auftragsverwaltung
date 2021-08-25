using Auftragsverwaltung.WPF.Commands;
using Auftragsverwaltung.WPF.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public class CommonViewModel : ViewModelBase
    {
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;
        private string _searchText;

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

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        public CommonViewModel()
        {
            SearchText = "Suchbegriff eingeben...";
        }

        protected void CommonDefautlView()
        {
            InputEnabled = false;
            SaveButtonEnabled = false;
            CreateButtonEnabled = true;
            ModifyButtonEnabled = true;
            DeleteButtonEnabled = true;
        }

        protected void CommonCreateView()
        {
            InputEnabled = true;
            SaveButtonEnabled = true;
            ModifyButtonEnabled = false;
            DeleteButtonEnabled = false;
        }

        protected void CommonModifyView()
        {
            InputEnabled = true;
            SaveButtonEnabled = true;
            CreateButtonEnabled = false;
            DeleteButtonEnabled = false;
        }


    }
}
