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

namespace GestionDuMateriel.View
{
    /// <summary>
    /// Logique d'interaction pour DetailleMeubles.xaml
    /// </summary>
    public partial class DetailleMeubles : UserControl
    {
        // constructeurs ... 
        private DetailleMeubles()
        {
            InitializeComponent();
        }
        public DetailleMeubles(MeublesVM meublesVM) : this()
        {
            DataContext = meublesVM;
        }

        public void ClosingParentWindow()
        {
            btnEnregMeuble.Command.Execute(null);
        }
    }
}
