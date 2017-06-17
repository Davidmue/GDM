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
    /// Logique d'interaction pour AfficheMeubles.xaml
    /// </summary>
    public partial class AfficheMeubles : Window
    {
        public AfficheMeubles()
        {
            InitializeComponent();
            DataContext = new MeublesVM(new PiecesVM(new PlansVM()), new EmployesVM()); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            btnEnregMateriel.Command.Execute(null);
        }

    }
}
