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
using GestionDuMateriel.ViewModel;

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour DetaillePieces.xaml
    /// </summary>
    public partial class DetaillePieces : UserControl
    {

        public DetaillePieces()
        {
            InitializeComponent();
            DataContext = new PiecesVM(new PlansVM());
        }

        public void ClosingParentWindow()
        {
            btnEnregMateriel.Command.Execute(null);
        }
    }
}
