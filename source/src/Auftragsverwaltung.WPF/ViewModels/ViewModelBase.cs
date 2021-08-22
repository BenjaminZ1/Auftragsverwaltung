using Auftragsverwaltung.WPF.Models;

namespace Auftragsverwaltung.WPF.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        private bool _inputEnabled;
        private bool _saveButtonEnabled;
        private bool _createButtonEnabled;
        private bool _modifyButtonEnabled;
        private bool _deleteButtonEnabled;

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
    }
}
