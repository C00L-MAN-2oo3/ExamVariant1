using ExamV1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExamV1.ViewModels
{
    public class AddClientViewModel
    {
        private ObservableCollection<Client> ClientList;
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string Country { get; set; }
        public double CostOfDay { get; set; }
        public double TransportationCost { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddClient());

        public AddClientViewModel(ObservableCollection<Client> ClientList)
        {
            this.ClientList = new ObservableCollection<Client>();
            this.ClientList = ClientList;
        }

        private void AddClient()
        {
            Client newClient = new Client
            {
                Id = ClientList.Count + 1,
                FirstName = FirstName,
                MiddleName = MiddleName,
                Passport = Passport,
                Country = Country,
                CostOfDay = CostOfDay,
                TransportationCost = TransportationCost,
                LastName = LastName,
                ArrivalDate = ArrivalDate,
                DepartureDate = DepartureDate,
            };

            ClientList.Add(newClient);
            MessageBox.Show("Запись добавлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
