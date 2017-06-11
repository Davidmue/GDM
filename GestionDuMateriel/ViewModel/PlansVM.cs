using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;


namespace GestionDuMateriel.ViewModel
{
    public class PlansVM : BaseViewModel
    {

        private ObservableCollection<Plan> _plans;
        private Plan _selection;

        public PlansVM()
        {
            _plans = new ObservableCollection<Plan>(App.Entities().Plans);
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
        private void DeleteExecute()
        {
            if (!(Selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Plans.Remove(Selection); 
                App.Entities().SaveChanges(); 
                // applique la suppression aux objets représentant les données ... 
                this.Plans.Remove(Selection);
                this.FirePropertyChanged("Plans");
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
                string pFullpath = System.IO.Path.Combine(p.CheminDossierSource.Trim(), p.NomFichier.Trim());
                if (string.Compare(pFullpath, CheminComplet, true) == 0) checkDoublon = true;
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
                this.Plans.Add(nouveauPlan);
                this.FirePropertyChanged("Plans");
            }
            
        }

    }

}
