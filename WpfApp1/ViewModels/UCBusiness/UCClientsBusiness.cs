using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCClientsBusiness : DependencyObject
    {
        private ClientRepository clientRepository;
        public bool ToSave { get; set; }
        public Models.ClientModel client { get; set; }

        public List<Models.ClientModel> ListOfClients
        {
            get { return (List<Models.ClientModel>)GetValue(ListOfClientsProperty); }
            set { SetValue(ListOfClientsProperty, value); }
        }
        public static readonly DependencyProperty ListOfClientsProperty = DependencyProperty.Register("ListOfClients", typeof(List<Models.ClientModel>), typeof(UCClientsBusiness), new PropertyMetadata(null));
        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand SaveCommand { get; set; }
        public UCClientsBusiness()
        {
            DeleteCommand = new ViewModelCommand(DeleteEtudiant, CanDeleteEtudiant);

            EditCommand = new ViewModelCommand(EditEtudiant, CanEditEtudiant);

            AddCommand = new ViewModelCommand(AddEtudiant, CanAddEtudiant);

            SaveCommand = new ViewModelCommand(SaveEtudiant, CanSaveEtudiant);

            clientRepository = new ClientRepository();

            ListOfClients = clientRepository.GetByAll();
        }
        private void DeleteEtudiant(object obj)
        {
            if (client != null)
            {
                clientRepository.Delete(client);

                ListOfClients = clientRepository.GetByAll();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a client !!! ");
            }

        }
        private void EditEtudiant(object obj)
        {

            if (client != null)
            {
                ToSave = false;

                etudiantDataEntry = new Views.DataEntry.ClientDataEntry();

                etudiantDataEntry.DataContext = this;

                etudiantDataEntry.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a student !!! ");
            }

        }
        private bool CanDeleteEtudiant(object obj)
        {
            return true;
        }
        private bool CanEditEtudiant(object obj)
        {
            return true;
        }
        private bool CanAddEtudiant(object obj)
        {
            return true;
        }
        public Views.DataEntry.ClientDataEntry etudiantDataEntry;
        private void AddEtudiant(object obj)
        {
            ToSave = true;

            client = new ClientModel();

            etudiantDataEntry = new Views.DataEntry.ClientDataEntry();

            etudiantDataEntry.DataContext = this;

            etudiantDataEntry.ShowDialog();

        }
        private void SaveEtudiant(object obj)
        {
            if (ToSave)
            {
                clientRepository.Add(client);
            }
            else
            {
                clientRepository.Update(client);
            }

            etudiantDataEntry.Close();

            ListOfClients = clientRepository.GetByAll();
        }
        private bool CanSaveEtudiant(object obj)
        {
            return true;
        }
    }
}
