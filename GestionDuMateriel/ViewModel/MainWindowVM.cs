using GestionDuMateriel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GestionDuMateriel.ViewModel
{

    public class MainWindowVM : BaseViewModel
    {
        private UserControl _contenu;
        private UserControl _contenuPlan;

        public ICommand AfficheDetaillePlans { get { return new ExecutableCommand(AffichePartiePlan); } }
        public void AffichePartiePlan()
        {
            Contenu = _contenuPlan; 
        }

        public void AjoutDuContenuPlan(UserControl ContenuPlan)
        {
            _contenuPlan = ContenuPlan; 
        }

        public UserControl Contenu
        {
            get
            {
                return _contenu;
            }
            set
            {
                _contenu = value;
                FirePropertyChanged("Contenu"); 
            }
        }
    }

    

}
