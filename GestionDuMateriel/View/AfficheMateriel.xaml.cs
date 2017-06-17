using GestionDuMateriel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour AfficheMateriel.xaml
    /// </summary>
    public partial class AfficheMateriel : Window
    {
        public AfficheMateriel()
        {
            InitializeComponent();
            DataContext = new MaterielsVM(new TypesMaterielVM());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //  (le bouton Enregistrer n'est pas visible (pas cliquable) par l'utilisateur ... 
            // appel de la commande "Enregistre" à la fermeture du formulaire ... 
            btnEnregMateriel.Command.Execute(null);
        }
    }
}
