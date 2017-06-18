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

        public MainWindow()
        {
            InitializeComponent();
            //
            // création des objets nécessaires qui restent toujours en mémoire ... 
            // création des sous-fenêtres ...
            _plansVM = new PlansVM();
            _detaillePlans = new DetaillePlans();
            _detaillePlans.DataContext = _plansVM;
            _piecesVM = new PiecesVM(_plansVM);
            _detaillePieces = new DetaillePieces();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // supprime les objets en mémoire ... 
            _detaillePlans.ClosingParentWindow();
            _detaillePieces.ClosingParentWindow();
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


    }
}
