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
    public class UpdateClientViewModel : BaseViewModel
    {
        private Client oldClient;
        private ObservableCollection<Client> ClientsList;
        public Client Client { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string Country { get; set; }
        public double CostOfDay { get; set; }
        public double TransportationCost { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }

        public RelayCommand UpdateCommand => new RelayCommand(execute => UpdateClient());

        public UpdateClientViewModel(Client client, ObservableCollection<Client> ClientsList)
        {
            oldClient = client;
            Client = client;
            this.ClientsList = ClientsList;

            FirstName = Client.FirstName;
            LastName = Client.LastName;
            MiddleName = Client.MiddleName;
            Passport = Client.Passport;
            Country = Client.Country;
            CostOfDay = Client.CostOfDay;
            TransportationCost = Client.TransportationCost;
            ArrivalDate = Client.ArrivalDate;
            DepartureDate = Client.DepartureDate;
        }

        private void UpdateClient()
        {
            Client.FirstName = FirstName;
            Client.LastName = LastName;
            Client.ArrivalDate = ArrivalDate;
            Client.DepartureDate = DepartureDate;
            Client.CostOfDay = CostOfDay;
            Client.TransportationCost = TransportationCost;
            Client.MiddleName = MiddleName;
            Client.Passport = Passport;
            Client.Country = Country;

            ClientsList.Remove(oldClient);
            ClientsList.Add(Client);
            MessageBox.Show("Запись обновлена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
