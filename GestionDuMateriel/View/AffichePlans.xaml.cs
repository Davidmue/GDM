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
using System.Windows.Shapes;
using GestionDuMateriel.View;
using System.IO;

// amélioration : pourrait enregistrer le ratio zoom ... 

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour AffichePlans.xaml
    /// </summary>
    public partial class AffichePlans : Window
    {

        private List<AffichePlan> _plansAffiches;
        private ImporterPlan _dlgImporterPlan;

#region interface

        public AffichePlans()
        {
            InitializeComponent();
            DataContext = new PlansVM();
            _plansAffiches = new List<AffichePlan>();
            _dlgImporterPlan = new ImporterPlan(); // la fenêtre de dialogue est créée 
            //                              en même temps que la fenêtre pour gérer les plans. 
        }

        public void FormulaireEnfantFerme(AffichePlan formulaireEnfant)
        {
            _plansAffiches.Remove(formulaireEnfant); 
        }

        public void RaffraichiGridView()
        {
            dgrPlans.Items.Refresh();
        }

        #endregion

        #region events

        private void btnAjouterUnPlan_Click(object sender, RoutedEventArgs e)
        {
            _dlgImporterPlan.ShowDialog();
            if(_dlgImporterPlan.InformationsValides)
            {
                (DataContext as PlansVM).AjoutPlan(_dlgImporterPlan.Description, _dlgImporterPlan.CheminComplet);
            }

        }

        private void btnAfficherLePlan_Click(object sender, RoutedEventArgs e)
        {
            // parcourt _plansAffiches à construire ...
            bool found = false; 
            PlansVM vm = (DataContext as PlansVM);
            if(!(vm.Selection == null)) {
                string path = (DataContext as PlansVM).Selection.CheminCompletDuFichier; 
                foreach (AffichePlan planDejaAffiche in _plansAffiches)
                {
                    if(planDejaAffiche.CheminCompletImagePlan == path)
                    {
                        if(planDejaAffiche.WindowState == WindowState.Minimized)
                        {
                            planDejaAffiche.WindowState = WindowState.Normal; 
                        }
                        planDejaAffiche.Focus(); 
                        found = true; 
                    }
                }
                if(!found)
                {
                    AffichePlanVM selectionDC = new AffichePlanVM((DataContext as PlansVM).Selection);
                    AffichePlan affichageDuPlan = new AffichePlan(selectionDC);
                    affichageDuPlan.FormulaireParent = this; 
                    affichageDuPlan.Show();
                    _plansAffiches.Add(affichageDuPlan); 
                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _dlgImporterPlan.DialogueSeraFerme = true; 
            _dlgImporterPlan.Close(); // la boîte de dialogue est fermée en même temps que 
            //                la fenêtre pour gérer les plans. 
            App.Entities().SaveChanges();
        }

        #endregion

    }
}
