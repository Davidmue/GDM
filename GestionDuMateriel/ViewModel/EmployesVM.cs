using GestionDuMateriel.Helpers;
using GestionDuMaterielDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace GestionDuMateriel.ViewModel
{
    public class EmployesVM : BaseViewModel
    {
        private ObservableCollection<Employe> _employes;
        private Employe _selection;

        public EmployesVM()
        {
            //
            _employes = new ObservableCollection<Employe>(App.Entities().Employes);
            // ... autres collections si nécessaire
            if (_employes.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _employes[0];
            }
        }

        public ObservableCollection<Employe> LesEmployes
        {
            get { return _employes; }
            set
            {
                _employes.Clear();
                _employes = value;
                FirePropertyChanged("LesEmployes");
            }
        }

        public Employe LEmploye
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LEmploye");
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Employes.Remove(LEmploye);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesEmployes.Remove(LEmploye);
                LEmploye = null;
                FirePropertyChanged("LEmploye");
                FirePropertyChanged("LesEmployes");
            }
        }


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            Employe nouvelEmploye = new Employe();
            nouvelEmploye.DateEntree = DateTime.Now;
            nouvelEmploye.EntreeDescription = "";
            nouvelEmploye.Nom = "";
            nouvelEmploye.Prenom = "";
            nouvelEmploye.DateSortie = null; 
            nouvelEmploye.SortieDescription = "";
            //
            // nouvelEmploye.Id  // Entity gère ceci ! 
            //
            App.Entities().Employes.Add(nouvelEmploye);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesEmployes.Add(nouvelEmploye);
            _selection = nouvelEmploye; // met à jour la sélection 
            FirePropertyChanged("LesEmployes");
            FirePropertyChanged("LEmploye");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}

