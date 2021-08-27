using Auftragsverwaltung.Application.Dtos;
using System.Windows;
using System.Windows.Controls;

namespace Auftragsverwaltung.WPF.Controls
{
    /// <summary>
    /// Interaction logic for CustomerListDetails.xaml
    /// </summary>
    public partial class CustomerListDetails : UserControl
    {
        public CustomerListDetails()
        {
            InitializeComponent();
        }

        public CustomerDto CustomerDto
        {
            get => (CustomerDto)GetValue(CustomerDtoProperty);
            set => SetValue(CustomerDtoProperty, value);
        }

        public static readonly DependencyProperty CustomerDtoProperty =
            DependencyProperty.Register("CustomerDto", typeof(CustomerDto), typeof(CustomerListDetails), new PropertyMetadata(null));

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (CustomerDto != null)
            {
                CustomerDto.Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
