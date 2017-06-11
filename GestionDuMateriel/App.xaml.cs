using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GestionDuMaterielDb.Model;

namespace GestionDuMateriel
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static GestionDuMaterielEntities _entities; // static -> pour appel sans instance

        public static GestionDuMaterielEntities Entities()
        {
            if (_entities == null) { _entities = new GestionDuMaterielEntities(); }
            return _entities;
        }

    }

}
