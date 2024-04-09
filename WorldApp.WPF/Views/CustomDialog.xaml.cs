using WorldApp.WPF.Database;
using WorldApp.WPF.Notifications;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorldApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public CustomDialog()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }

        private void CloseButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            WeakReferenceMessenger.Default.Send(new RefreshNotification(true));
        }
    }
}