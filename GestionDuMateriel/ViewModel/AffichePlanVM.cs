using GestionDuMateriel.Helpers;
using GestionDuMaterielDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionDuMateriel.ViewModel
{
    public class AffichePlanVM : BaseViewModel
    {

        private string _cheminCompletImagePlan;
        private ObservableCollection<Plan> _plans;
        private Plan _selection = null;

        public AffichePlanVM()
        {
            _plans = new ObservableCollection<Plan>(App.Entities().Plans);
        }

        public string CheminCompletImagePlan
        {
            get { return _cheminCompletImagePlan; }
            set { _cheminCompletImagePlan = value;
                foreach (Plan p in _plans)
                {
                    if(string.Compare(_cheminCompletImagePlan, p.CheminCompletDuFichier, true) == 0)
                    {
                        _selection = p;
                        FirePropertyChanged("CheminCompletImagePlan");
                        FirePropertyChanged("DescriptionImagePlan");
                        FirePropertyChanged("RatioAffichage");
                    }
                }
            }
        }

        public string DescriptionImagePlan
        {
            get { return _selection == null ? "Autre plan (pas dans la base de données)" : _selection.Description; }
        }

        public double RatioAffichage
        {
            get
            {
                return _selection == null ? 1.0 : _selection.RatioAffichage;
            }
            set
            {
                if(!(_selection == null))
                {
                    _selection.RatioAffichage = value; 
                }
            }
        }

    }
}
