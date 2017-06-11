using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

// dialogue

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour ImporterPlan.xaml
    /// </summary>
    public partial class ImporterPlan : Window
    {


        #region interface

        public bool InformationsValides { get; set; }

        public bool DialogueSeraFerme { get; set; }

        public string Description
        {
            get { return this.tbxDescription.Text; }
        }

        public string CheminComplet
        {
            get { return this.tbxFullpath.Text; }
        }

        public ImporterPlan()
        {
            InitializeComponent();
            DialogueSeraFerme = false; 
        }

        #endregion

        #region events

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                tbxFullpath.Text = openFileDialog.FileName;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); 
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(tbxDescription.Text))
            {
                MessageBox.Show("SVP, remplissez le champ \"Description\" !"); 
            }
            else if(string.IsNullOrEmpty(tbxFullpath.Text))
            {
                MessageBox.Show("SVP, sélectionnez le chemin complet du fichier !");
            }
            else
            {
                if(File.Exists(tbxFullpath.Text))
                {
                    InformationsValides = true;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Le chemin complet du fichier n'est pas valide !");
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            InformationsValides = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(! DialogueSeraFerme)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    #endregion

    }
}
