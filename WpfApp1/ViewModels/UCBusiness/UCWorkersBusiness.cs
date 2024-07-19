using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repositories;

namespace WpfApp1.ViewModels.UCBusiness
{
    public class UCWorkersBusiness : DependencyObject
    {
        private WorkerRepository workerRepository;
        public bool ToSave { get; set; }
        public Models.WorkerModel Worker { get; set; }

        public List<Models.WorkerModel> ListOfWorkers
        {
            get { return (List<Models.WorkerModel>)GetValue(ListOfWorkersProperty); }
            set { SetValue(ListOfWorkersProperty, value); }
        }
        public static readonly DependencyProperty ListOfWorkersProperty = DependencyProperty.Register("ListOfWorkers", typeof(List<Models.WorkerModel>), typeof(UCWorkersBusiness), new PropertyMetadata(null));
        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand SaveCommand { get; set; }
        public UCWorkersBusiness()
        {
            DeleteCommand = new ViewModelCommand(DeleteEtudiant, CanDeleteEtudiant);

            EditCommand = new ViewModelCommand(EditEtudiant, CanEditEtudiant);

            AddCommand = new ViewModelCommand(AddEtudiant, CanAddEtudiant);

            SaveCommand = new ViewModelCommand(SaveEtudiant, CanSaveEtudiant);

            workerRepository = new WorkerRepository();

            ListOfWorkers = workerRepository.GetByAll();
        }
        private void DeleteEtudiant(object obj)
        {
            if (Worker != null)
            {
                workerRepository.Delete(Worker);

                ListOfWorkers = workerRepository.GetByAll();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a student !!! ");
            }

        }
        private void EditEtudiant(object obj)
        {

            if (Worker != null)
            {
                ToSave = false;

                etudiantDataEntry = new Views.DataEntry.WorkerDataEntry();

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
        public Views.DataEntry.WorkerDataEntry etudiantDataEntry;
        private void AddEtudiant(object obj)
        {
            ToSave = true;

            Worker = new WorkerModel();

            etudiantDataEntry = new Views.DataEntry.WorkerDataEntry();

            etudiantDataEntry.DataContext = this;

            etudiantDataEntry.ShowDialog();

        }
        private void SaveEtudiant(object obj)
        {
            if (ToSave)
            {
                workerRepository.Add(Worker);
            }
            else
            {
                workerRepository.Update(Worker);
            }

            etudiantDataEntry.Close();

            ListOfWorkers = workerRepository.GetByAll();
        }
        private bool CanSaveEtudiant(object obj)
        {
            return true;
        }
    }
}

