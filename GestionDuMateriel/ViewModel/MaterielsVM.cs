using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;
using System.Windows.Input;

namespace GestionDuMateriel.ViewModel
{
    public class MaterielsVM : BaseViewModel
    {

        private ObservableCollection<Materiel> _materiels;
        private Materiel _selection;

        public MaterielsVM()
        {
            _materiels = new ObservableCollection<Materiel>(App.Entities().Materiels);
            // ... autres collections si nécessaire
            if (_materiels.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _materiels[0];
            }
        }

        public ObservableCollection<Materiel> LesMateriels
        {
            get { return _materiels; }
            set
            {
                _materiels.Clear();
                _materiels = value;
                FirePropertyChanged("LesMateriels");
            }
        }

        public Materiel LeMateriel
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LeMateriel");
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        private void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Materiels.Remove(LeMateriel);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesMateriels.Remove(LeMateriel);
                FirePropertyChanged("LesMateriels");
            }
        }


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        private void Ajout()
        {
            Materiel nouveauMateriel = new Materiel();
            nouveauMateriel.Achat = DateTime.Now;
            nouveauMateriel.CodeBarre = "à remplacer";
            nouveauMateriel.Description = "à remplacer";
            nouveauMateriel.DescriptionEntree = "";
            nouveauMateriel.DescriptionSortie = "";
            nouveauMateriel.Entree = DateTime.Now;
            nouveauMateriel.Garantie = false;
            nouveauMateriel.GarantieEcheance = DateTime.Now;
            nouveauMateriel.Id = 0;
            nouveauMateriel.PrixAchat = new decimal(0.0);
            nouveauMateriel.Sortie = null;
            // nouveauMateriel.TypeMaterielId = 
            App.Entities().Materiels.Add(nouveauMateriel);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesMateriels.Add(nouveauMateriel);
            FirePropertyChanged("LesMateriels");
            // met à jour la sélection 
            _selection = nouveauMateriel;
            FirePropertyChanged("LeMateriel");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
