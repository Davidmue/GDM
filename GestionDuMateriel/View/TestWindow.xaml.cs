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

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
            //Aide _aide = new Aide();
            //uniqueGrid.Children.Add(_aide);

            RondesVM _rondesVM = new RondesVM();
            CodesBarreVM _codesBarreVM = new CodesBarreVM(_rondesVM);
            uniqueGrid.Children.Add(new DetailleCodesBarre(_codesBarreVM));

            //uniqueGrid.Children.Add(new DetailleCodesBarre());
        }

    }
}
