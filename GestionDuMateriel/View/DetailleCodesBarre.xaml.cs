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
    /// Logique d'interaction pour DetailleCodesBarre.xaml
    /// </summary>
    public partial class DetailleCodesBarre : UserControl
    {

        // constructeurs ...
        private DetailleCodesBarre()
        {
            InitializeComponent();
        }
        public DetailleCodesBarre(CodesBarreVM CodesBarreVMdependance) : this()
        {
            DataContext = CodesBarreVMdependance;
        }

        public void ClosingParentWindow()
        {
            //  (le bouton Enregistrer n'est pas visible (pas cliquable) par l'utilisateur ... 
            // appel de la commande "Enregistre" à la fermeture du formulaire ... 

            btnOk.Command.Execute(null);

        }
        
    }
}
