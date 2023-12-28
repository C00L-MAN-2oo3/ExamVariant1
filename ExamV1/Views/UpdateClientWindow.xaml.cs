using ExamV1.Models;
using ExamV1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ExamV1.Views
{
    /// <summary>
    /// Логика взаимодействия для UpdateClientWindow.xaml
    /// </summary>
    public partial class UpdateClientWindow : Window
    {
        public UpdateClientWindow(Client selectedClient, ObservableCollection<Client> ClientsList)
        {
            UpdateClientViewModel updateClientViewModel = new UpdateClientViewModel(selectedClient, ClientsList);
            InitializeComponent();
            DataContext = updateClientViewModel;
        }
    }
}
