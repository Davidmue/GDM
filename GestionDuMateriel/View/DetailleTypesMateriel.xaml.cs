﻿using GestionDuMateriel.ViewModel;
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
    /// Logique d'interaction pour DetailleTypesMateriel.xaml
    /// </summary>
    public partial class DetailleTypesMateriel : UserControl
    {
        private DetailleTypesMateriel()
        {
            InitializeComponent();
        }
        public DetailleTypesMateriel(TypesMaterielVM typeMaterielVM) : this()
        {
            DataContext = typeMaterielVM;
        }

        public void ClosingParentWindow()
        {
            btnEnregistrer.Command.Execute(null);
        }
    }
}