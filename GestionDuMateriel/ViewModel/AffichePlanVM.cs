using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMateriel.ViewModel
{
    public class AffichePlanVM 
    {

        private string _cheminCompletImagePlan;
        private string _descriptionImagePlan;

        public string CheminCompletImagePlan
        {
            get { return _cheminCompletImagePlan; }
            set { _cheminCompletImagePlan = value; }
        }

        public string DescriptionImagePlan
        {
            get { return _descriptionImagePlan; }
            set { _descriptionImagePlan = value; }
        }

    }
}
