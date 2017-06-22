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

        // constructeurs ... 
        private ImporteCodelsBarre()
        {
            InitializeComponent();
        }
        public ImporteCodelsBarre(RondesVM rondeVM) : this()
        {
            DataContext = rondeVM;
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
                tbxCheminFichier.Focus(); 
                tbxCheminFichier.Text = cheminComplet;
                btnChoisiFichier.Focus();  

                //tbxFile.Text = System.IO.Path.GetFileName(cheminComplet);
                //tbxFolder.Text = System.IO.Path.GetDirectoryName(cheminComplet);


//                (DataContext as RondesVM).LaRonde.ImportationCheminDossierSource = System.IO.Path.GetDirectoryName(cheminComplet);
//                (DataContext as RondesVM).LaRonde.ImportationNomFichier = System.IO.Path.GetFileName(cheminComplet);
                // tbxCheminFichier.Text = (DataContext as RondesVM).LaRonde.ImportationCheminCompletFichier; 
            }
            // btnMettreAJour.Command.Execute(null); 
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }

        private void btnVoirContenu_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void btnImporter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((DataContext as RondesVM).LaRonde.ImportationNomFichier);
            MessageBox.Show((DataContext as RondesVM).LaRonde.ImportationCheminDossierSource);
            this.Hide();
        }
    }
}