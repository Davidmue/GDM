using GestionDuMateriel.View;
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

namespace GestionDuMateriel
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PlansVM _plansVM; 
        private DetaillePlans _detaillePlans;
        private PiecesVM _piecesVM; 
        private DetaillePieces _detaillePieces;
        private MeublesVM _meublesVM;
        private DetailleMeubles _detailleMeubles;
        private EmployesVM _employesVM; 
        private DetailleEmployes _detailleEmployes;
        private TypesMaterielVM _typesMaterielVM;
        private DetailleTypesMateriel _detailleTypesMateriel; 

        public MainWindow()
        {
            InitializeComponent();
            //
            // création des objets nécessaires qui restent toujours en mémoire ... 
            // création des sous-fenêtres ...
            _plansVM = new PlansVM();
            _detaillePlans = new DetaillePlans(_plansVM);
            _piecesVM = new PiecesVM(_plansVM);
            _detaillePieces = new DetaillePieces(_piecesVM);
            _employesVM = new EmployesVM();
            _detailleEmployes = new DetailleEmployes(_employesVM);
            _meublesVM = new MeublesVM(_piecesVM, _employesVM);
            _detailleMeubles = new DetailleMeubles(_meublesVM);
            _typesMaterielVM = new TypesMaterielVM();
            _detailleTypesMateriel = new DetailleTypesMateriel(_typesMaterielVM);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // supprime les objets dans l'ordre inverse de création ... 
            _detailleTypesMateriel.ClosingParentWindow();
            _detailleMeubles.ClosingParentWindow();
            _detaillePieces.ClosingParentWindow();
            _detaillePlans.ClosingParentWindow();
            _detailleEmployes.ClosingParentWindow();
        }

        private void QuitAppMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close(); 
        }

        private void btnPlans_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detaillePlans);
        }

        private void btnPieces_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detaillePieces);
        }

        private void btnMeubles_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleMeubles);
        }

        private void btnEmployes_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleEmployes);
        }

        private void btnTypes_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleTypesMateriel);
        }

    }
}
