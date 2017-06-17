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

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour MeubleDetail.xaml
    /// </summary>
    public partial class MeubleDetail : UserControl
    {
        public MeubleDetail()
        {
            InitializeComponent();
        }

        private void cbxEployeEstNull_Checked(object sender, RoutedEventArgs e)
        {
            if(((bool)cbxEployeEstNull.IsChecked))
            {
                cbxEmploye.SelectedItem = null; 
                cbxEmploye.UpdateLayout();
            }
        }

        private void cbxEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxEployeEstNull.IsChecked = false; 
            cbxEployeEstNull.UpdateLayout(); 
        }
    }
}
