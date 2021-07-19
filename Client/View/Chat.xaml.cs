using Client.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatViewModel VM;
        public MainWindow()
        {
            VM = new ChatViewModel();
            DataContext = VM;
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            VM.SendMessage(VM.InputMessage, VM.Id);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VM.DisconnectUser();
        }

        private void ConBtn_Click(object sender, RoutedEventArgs e)
        {
            if (VM.IsConnected)
            {
                VM.DisconnectUser();
            }
            else
            {
                VM.ConnectUser();
            }
        }
    }
}
