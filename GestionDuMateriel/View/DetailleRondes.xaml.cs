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

        // ici

        public DetailleRondes(PresencesVM PresencesVMDependance) : this()
        {
            DataContext = PresencesVMDependance;
        }

        public void ClosingParentWindow()
        {
            //  (le bouton Enregistrer n'est pas visible (pas cliquable) par l'utilisateur ... 
            // appel de la commande "Enregistre" à la fermeture du formulaire ... 
            btnEnregRondes.Command.Execute(null);

        }

        private void btnImporterScans_Click(object sender, RoutedEventArgs e)
        {
            btnNouvelleRonde.Command.Execute(null);
            _dlgImportCodesBarre = new ImporteCodelsBarre((DataContext as PresencesVM));
            _dlgImportCodesBarre.ShowDialog();
            //
            // do the job ...
            //
            _dlgImportCodesBarre.DialogueSeraFerme = true;
            _dlgImportCodesBarre.Close(); // la boîte de dialogue est fermée en même temps que 
            //
            btnAnnulerLImporte.Command.Execute(null);
            dagRondes.Items.Refresh(); 
        }

        // SelectionChanged="dagTypeMateriel_SelectionChanged"

    }
}
