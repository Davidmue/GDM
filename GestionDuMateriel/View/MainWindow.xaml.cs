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
        //
        private MainWindowVM _mainWindowWM;
        //
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
        private MaterielsVM _materielsVM;
        private DetailleMateriels _detailleMateriels;
        private RondesVM _rondesVM;
        private CodesBarreVM _codesBarreVM;
        private DetailleCodesBarre _detailleCodesBarre;
        private DetailleRondes _detailleRondes;
        private PresencesVM _presencesVM;
        //
        private Options _options;
        private Aide _aide;
        private Apropos _aPropos;
        //

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowWM = new MainWindowVM(); 
            //
            // création des objets nécessaires qui restent toujours en mémoire ... 
            // création des sous-fenêtres ...
            _plansVM = new PlansVM();
            _detaillePlans = new DetaillePlans(_plansVM);
            _mainWindowWM.AjoutDuContenuPlan(_detaillePlans);
            _piecesVM = new PiecesVM(_plansVM);
            _detaillePieces = new DetaillePieces(_piecesVM);
            _employesVM = new EmployesVM();
            _detailleEmployes = new DetailleEmployes(_employesVM);
            _meublesVM = new MeublesVM(_piecesVM, _employesVM);
            _detailleMeubles = new DetailleMeubles(_meublesVM);
            _typesMaterielVM = new TypesMaterielVM();
            _detailleTypesMateriel = new DetailleTypesMateriel(_typesMaterielVM);
            _materielsVM = new MaterielsVM(_typesMaterielVM);
            _detailleMateriels = new DetailleMateriels(_materielsVM);
            _rondesVM = new RondesVM();
            _codesBarreVM = new CodesBarreVM(_rondesVM);
            _detailleCodesBarre = new DetailleCodesBarre(_codesBarreVM);
            _presencesVM = new PresencesVM(_rondesVM, _materielsVM, _meublesVM, _codesBarreVM);
            _detailleRondes = new DetailleRondes(_presencesVM);
            //
            _aPropos = new Apropos();
            _aide = new Aide();
            _options = new Options();
            //
            DataContext = _mainWindowWM; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // AfficheDetailleAide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // rien à faire pour _aide et _aPropos _options pour le moment ... 
            // supprime les objets dans l'ordre inverse de création ... 

            _detailleRondes.ClosingParentWindow();
            _detailleCodesBarre.ClosingParentWindow();
            _detailleMateriels.ClosingParentWindow();
            _detailleTypesMateriel.ClosingParentWindow();
            _detailleMeubles.ClosingParentWindow();
            _detailleEmployes.ClosingParentWindow();
            _detaillePieces.ClosingParentWindow();
            _detaillePlans.ClosingParentWindow();
        }

        // events du menu ... 
        private void QuitAppMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AtteindrePlansMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AfficheDetaillePlans();
        }


        private void btnPlans_Click(object sender, RoutedEventArgs e)
        {
            AfficheDetaillePlans();
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

        private void btnMateriels_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleMateriels);
        }

        private void btnRondes_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleRondes);
        }

        //

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            AfficheDetailleAide();
        }

        private void btnApropos_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_aPropos);
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_options);
        }

        private void btnCodesBarre_Click(object sender, RoutedEventArgs e)
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detailleCodesBarre);
        }

        private void AfficheDetailleAide()
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_aide);
        }

        private void AfficheDetaillePlans()
        {
            spaContent.Children.Clear();
            spaContent.Children.Add(_detaillePlans);
        }

    }
}
