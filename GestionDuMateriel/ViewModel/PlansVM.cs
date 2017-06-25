using System;
using System.Windows;
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
    public class PlansVM : BaseViewModel
    {

        private ObservableCollection<Plan> _plans;
        private Plan _selection;

        public PlansVM()
        {
            _plans = new ObservableCollection<Plan>(App.Entities().Plans.OrderBy(p => p.Description));
            // ... autres collections si nécessaire
            if(_plans.Count == 0)
            {
                _selection = null; 
            } 
            else
            {
                _selection = _plans[0]; 
            }
        }

        public ObservableCollection<Plan> Plans
        {
            get { return _plans; }
            set
            {
                _plans.Clear();
                _plans = value;
                FirePropertyChanged("Plans"); 
            }
        }

        public Plan Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("Selection");
            }
        }

        public ICommand Delete { get { return new ExecutableCommand(DeleteExecute); } }
        public void DeleteExecute()
        {
            if (!(Selection == null))
            {
                if(Selection.Pieces.Count == 0)
                {
                    // applique la suppression à la base de données ... 
                    App.Entities().Plans.Remove(Selection);
                    App.Entities().SaveChanges();
                    // applique la suppression aux objets représentant les données ... 
                    Plans.Remove(Selection);
                    _selection = null;
                    FirePropertyChanged("Selection");
                    FirePropertyChanged("Plans");
                }
                else
                {
                    MessageBox.Show("Le plan est encore lié à une ou plusieurs pièces. ", "Suppression impossible", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        public void AjoutPlan(string Description, string CheminComplet)
        {
            Plan nouveauPlan = new Plan();
            nouveauPlan.Description = Description;
            nouveauPlan.CheminDossierSource = System.IO.Path.GetDirectoryName(CheminComplet);
            nouveauPlan.NomFichier = System.IO.Path.GetFileName(CheminComplet);
            nouveauPlan.DateImport = DateTime.Now;
            nouveauPlan.RatioAffichage = 1.0;
            // pré-requis : pas de doublon ! 
            bool checkDoublon = false;
            foreach (Plan p in Plans)
            {
                if (string.Compare(p.CheminCompletDuFichier, CheminComplet, true) == 0) checkDoublon = true;
            }
            if(checkDoublon)
            {
                MessageBox.Show("Impossible de mettre à jour la base de données ! (" + 
                    "doublon détecté" + ")", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // applique la suppression à la base de données ... 
                App.Entities().Plans.Add(nouveauPlan); 
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                Plans.Add(nouveauPlan);
                _selection = nouveauPlan; // met à jour la sélection 
                FirePropertyChanged("Plans");
                FirePropertyChanged("Selection");
            }
            
        }

    }

}
