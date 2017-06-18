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
using GestionDuMateriel.Helpers;
using GestionDuMateriel.ViewModel;
using System.IO;

// fenêtre dépendante de la fenêtre DetaillePlans ! 

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour AffichePlan.xaml
    /// </summary>
    public partial class AffichePlan : Window
    {
        private bool firstActivationDone = false;
        private bool ratioIsInitialized = false;
        private double ratio;
        private double originalWidth = 0;
        private double originalHeight = 0;
        private DetaillePlans _formulaireParent = null;

        #region interface

        public AffichePlan(AffichePlanVM DataContextRef)
        {
            InitializeComponent();
            DataContext = DataContextRef;
        }

        public string CheminCompletImagePlan
        {
            get { return (DataContext as AffichePlanVM).CheminCompletImagePlan; }
        }

        public string DescriptionImagePlan
        {
            get { return (DataContext as AffichePlanVM).DescriptionImagePlan; }
        }

        public DetaillePlans FormulaireParent
        {
            get { return _formulaireParent; }
            set
            {
                _formulaireParent = value;
            }
        }

        #endregion

        #region events

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (!ratioIsInitialized) setRatio();
            if (ratio < 10) ratio += 0.1;
            RefreshImage();
            (DataContext as AffichePlanVM).RatioAffichage = ratio;
            _formulaireParent.RaffraichiGridView();
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (!ratioIsInitialized) setRatio();
            if (ratio > 0.15) ratio -= 0.1;
            RefreshImage();
            (DataContext as AffichePlanVM).RatioAffichage = ratio;
            _formulaireParent.RaffraichiGridView();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!firstActivationDone)
            {
                if (!(File.Exists(CheminCompletImagePlan)))
                {
                    MessageBox.Show(CheminCompletImagePlan, "Fichier absent", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    setRatio();
                    RefreshImage();
                }
                firstActivationDone = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            FormulaireParent.FormulaireEnfantFerme(this);
        }

        #endregion

        private void setRatio()
        {
            ratio = (DataContext as AffichePlanVM).RatioAffichage;
            // catch original size ...
            this.FichierImagePlan.Width = this.FichierImagePlanInvisible.Width;
            this.FichierImagePlan.Height = this.FichierImagePlanInvisible.Height;
            this.FichierImagePlan.UpdateLayout();
            originalWidth = this.FichierImagePlan.RenderSize.Width;
            originalHeight = this.FichierImagePlan.RenderSize.Height;
            ratioIsInitialized = true;
        }

        private void RefreshImage()
        {
            this.FichierImagePlan.Width = (originalWidth * ratio);
            this.FichierImagePlan.Height = (originalHeight * ratio);
            this.FichierImagePlan.UpdateLayout();
        }

    }
}
