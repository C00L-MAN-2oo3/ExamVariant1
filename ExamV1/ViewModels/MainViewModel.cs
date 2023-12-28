using ExamV1.Models;
using ExamV1.Properties;
using ExamV1.Views;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Windows;
using System.Windows.Data;

namespace ExamV1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Client selectedClient;
        public ObservableCollection<Client> ClientList { get; set; }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddCommand => new RelayCommand(execute => AddClient());
        public RelayCommand UpdateCommand => new RelayCommand(execute => UpdateClient(), canExecute => selectedClient != null);
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteClient(), canExecute => selectedClient != null);
        public RelayCommand OpenCommand => new RelayCommand(execute => OpenFile());
        public RelayCommand SaveCommand => new RelayCommand(execute => SaveFile());

        public MainViewModel()
        {           
            string path = Path.Combine(Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Parent.FullName, "Resources\\Clients.json");
            
            if (ClientList == null)
                LoadData(path);
        }

        private async void LoadData(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string jsonFile = await reader.ReadToEndAsync();
                    ClientList = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonFile);
                }
            }
            catch (Exception ex)
            {
                ClientList = new ObservableCollection<Client>();

                MessageBox.Show($"Произошла ошибка!\nПодробнее: {ex.ToString().ToLower()}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                for (int i = 0; i < 5; i++)
                    ClientList.Add(new Client()
                    {
                        Id = ClientList.Count + 1,
                        LastName = "Unknow",
                        FirstName = "Unknow",
                        MiddleName = "Unknow",
                        Passport = "Unknow",
                        Country = "Unknow",
                        CostOfDay = 0.0,
                        TransportationCost = 0.0,
                        ArrivalDate = "01/01/01",
                        DepartureDate = "01/01/01"
                    });
            }
        }

        private void AddClient()
        {
            AddClientWindow addClientWindow = new AddClientWindow(ClientList);
            addClientWindow.ShowDialog();
        }

        private void UpdateClient()
        {
            UpdateClientWindow updateClientWindow = new UpdateClientWindow(selectedClient, ClientList);
            updateClientWindow.ShowDialog();
        }

        private void DeleteClient()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ClientList.Remove(selectedClient);
                MessageBox.Show("Запись удалена!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                selectedClient = null;
            }
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.json | *.json";
            openFileDialog.Title = "Выберите файл формата .json";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                LoadData(path);
            }
        }

        private void SaveFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "*.json | *.json";
            saveFile.Title = "Сохраните файл формата .json";

            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    string jsonFile = JsonConvert.SerializeObject(ClientList, Formatting.Indented);

                    File.WriteAllText(saveFile.FileName, jsonFile);
                    MessageBox.Show("Данные сохранены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка!\nПодробнее: {ex.ToString().ToLower()}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
