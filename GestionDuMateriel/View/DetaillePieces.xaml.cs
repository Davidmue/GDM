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

        // constructeurs ... 
        private DetaillePieces()
        {
            InitializeComponent();            
        }
        public DetaillePieces(PiecesVM pieceVM) : this()
        {
            DataContext = pieceVM; 
        }

        public void ClosingParentWindow()
        {
            btnEnregMateriel.Command.Execute(null);
        }

        private void btnAjoutMateriel_Click(object sender, RoutedEventArgs e)
        {
            btnAjoutMaterielInvisible.Command.Execute(null);
            PieceDetails.tbxDescriptionPiece.Focus(); 
        }
    }
}
