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
        // event Changed;

        private Plan _selection = null;

        // get the reference ... 
        public AffichePlanVM(Plan Selection)
        {
            _selection = Selection; 
        }

        public string CheminCompletImagePlan
        {
            get { return _selection.CheminCompletDuFichier; }
        }

        public string DescriptionImagePlan
        {
            get { return _selection.Description; }
        }

        public double Id
        {
            get { return _selection.Id; }
        }

        public string DateImportFormatee
        {
            get { return _selection.DateImport.ToString(); }
        }

        public string RatioDeuxVirgules
        {
            get
            {
                return (Math.Round(_selection.RatioAffichage, 2)).ToString();
            }
        }

        public double RatioAffichage
        {
            get
            {
                return _selection.RatioAffichage;
            }
            set
            {
                _selection.RatioAffichage = value;
                FirePropertyChanged("RatioAffichage");
                FirePropertyChanged("RatioDeuxVirgules");
            }
        }

    }
}
