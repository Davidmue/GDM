using GestionDuMateriel.ViewModel;
using GestionDuMaterielDb.Model;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour ImporteCodelsBarre.xaml
    /// </summary>
    public partial class ImporteCodelsBarre : Window
    {
        private bool _dialogueSeraFerme;
        private PresencesVM _presenceVM;

        // constructeurs ... 
        private ImporteCodelsBarre()
        {
            InitializeComponent();
        }
        public ImporteCodelsBarre(PresencesVM presenceVM) : this()
        {
            DataContext = presenceVM;
            _presenceVM = presenceVM; 
            _dialogueSeraFerme = false;
        }

        private void dlgImportCodesBarre_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!DialogueSeraFerme)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        public bool DialogueSeraFerme
        {
            get { return _dialogueSeraFerme; }
            set { _dialogueSeraFerme = value; }
        }

        private void btnChoisiFichier_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string cheminComplet = openFileDialog.FileName;
                tbxCheminFichier.Focus(); // pour que les bindings fonctionnent, il faut sélectionner le contrôle ! 
                tbxCheminFichier.Text = cheminComplet;
                btnChoisiFichier.Focus();  // retour sélection
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnImporter_Click(object sender, RoutedEventArgs e)
        {
            btnDemarreProcedureImport.Command.Execute(null);
            this.Hide();
        }
    }
}