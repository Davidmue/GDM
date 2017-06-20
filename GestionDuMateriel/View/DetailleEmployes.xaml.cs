using GestionDuMateriel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour DetailleEmployes.xaml
    /// </summary>
    public partial class DetailleEmployes : UserControl
    {
        // constructeurs ...
        private DetailleEmployes()
        {
            InitializeComponent();
        }
        public DetailleEmployes(EmployesVM employeVM) : this()
        {
            DataContext = employeVM;
        }

        public void ClosingParentWindow()
        {
            //  (le bouton Enregistrer n'est pas visible (pas cliquable) par l'utilisateur ... 
            // appel de la commande "Enregistre" à la fermeture du formulaire ... 
            btnEnregistreEmploye.Command.Execute(null);
        }
    }
}
