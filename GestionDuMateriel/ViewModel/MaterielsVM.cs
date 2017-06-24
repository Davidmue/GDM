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
        private TypesMaterielVM _typesMaterielVmDependance; 

        private List<Materiel> _resultatDeRechercheAvecBarreCode;

        public MaterielsVM(TypesMaterielVM TypesMaterielVmDependance)
        {
            _resultatDeRechercheAvecBarreCode = new List<Materiel>();
            //
            // Materiel utilise TypeMateriel ... 
            _typesMaterielVmDependance = TypesMaterielVmDependance;
            //
            _materiels = new ObservableCollection<Materiel>(App.Entities().Materiels.OrderBy(m => m.TypeMateriel.Description).ThenBy(m => m.Description));
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

        public ObservableCollection<TypeMateriel> LesTypesMateriel
        {
            get
            {
                return _typesMaterielVmDependance.TypesMateriel;
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

        public void RechercheAvecBarreCode(string CodeBarre)
        {
            _resultatDeRechercheAvecBarreCode = new List<Materiel>(LesMateriels.Where(m => m.CodeBarre.Trim() == CodeBarre.Trim()));
        }
        public Materiel ResultatDeRechercheAvecBarreCode()
        {
            if (_resultatDeRechercheAvecBarreCode.Count == 0)
            {
                return null;
            }
            else
            {
                return _resultatDeRechercheAvecBarreCode[0];
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Materiels.Remove(LeMateriel);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesMateriels.Remove(LeMateriel);
                LeMateriel = null;
                FirePropertyChanged("LeMateriel"); 
                FirePropertyChanged("LesMateriels");
            }
        }


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            Materiel nouveauMateriel = new Materiel();
            nouveauMateriel.Achat = DateTime.Now;
            nouveauMateriel.CodeBarre = "";
            nouveauMateriel.Description = "";
            nouveauMateriel.DescriptionEntree = "";
            nouveauMateriel.DescriptionSortie = "";
            nouveauMateriel.Entree = DateTime.Now;
            nouveauMateriel.Garantie = false;
            nouveauMateriel.GarantieEcheance = DateTime.Now;
            //
            // nouveauMateriel.Id  // Entity gère ceci ! 
            //
            nouveauMateriel.PrixAchat = new decimal(0.0);
            nouveauMateriel.Sortie = null;
            //
            // un type au minimum doit être créé ...
            ObservableCollection<TypeMateriel> typesMateriel = _typesMaterielVmDependance.TypesMateriel;
            if(typesMateriel.Count == 0)
            {
                _typesMaterielVmDependance.NouveauType(); 
            }
            TypeMateriel typeMateriel = _typesMaterielVmDependance.TypeMaterielSelection;
            nouveauMateriel.TypeMateriel = typeMateriel; 
            //
            // nouveauMateriel.TypeMaterielId = 
            App.Entities().Materiels.Add(nouveauMateriel);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesMateriels.Add(nouveauMateriel);
            _selection = nouveauMateriel; // met à jour la sélection 
            FirePropertyChanged("LesMateriels");
            FirePropertyChanged("LeMateriel");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
