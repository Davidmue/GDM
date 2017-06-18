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
    /// Logique d'interaction pour AfficheRondes.xaml
    /// </summary>
    public partial class AfficheRondes : Window
    {
        public AfficheRondes()
        {
            InitializeComponent();
            DataContext = new RondesVM(); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnEnregRondes.Command.Execute(null);
        }

        private void dagTypeMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CodesBarreSelonRondeDetails.DataContext = new CodesBarreVM( (DataContext as RondesVM) );
            CodesBarreSelonRondeDetails.dgrCodesBarre.Items.Refresh(); 
        }
    }
}
