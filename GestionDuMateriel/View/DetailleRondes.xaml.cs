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
    /// Logique d'interaction pour DetailleRondes.xaml
    /// </summary>
    public partial class DetailleRondes : UserControl
    {
        private ImporteCodelsBarre _dlgImportCodesBarre;

        // constructeurs ... 
        private DetailleRondes()
        {
            InitializeComponent();
        }
        public DetailleRondes(RondesVM rondesVM) : this()
        {
            DataContext = rondesVM;
            _dlgImportCodesBarre = new ImporteCodelsBarre(rondesVM);
        }

        public void ClosingParentWindow()
        {
            //  (le bouton Enregistrer n'est pas visible (pas cliquable) par l'utilisateur ... 
            // appel de la commande "Enregistre" à la fermeture du formulaire ... 
            btnEnregRondes.Command.Execute(null);
            _dlgImportCodesBarre.DialogueSeraFerme = true;
            _dlgImportCodesBarre.Close(); // la boîte de dialogue est fermée en même temps que 

        }

        private void btnImporterScans_Click(object sender, RoutedEventArgs e)
        {
            btnImporterScans.Command.Execute(null);
            
            _dlgImportCodesBarre.ShowDialog(); 
        }

        // SelectionChanged="dagTypeMateriel_SelectionChanged"

    }
}
