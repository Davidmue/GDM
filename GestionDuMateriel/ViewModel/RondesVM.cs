using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;
using System.Windows.Input;
using System.Windows;
using System.IO;


namespace GestionDuMateriel.ViewModel
{
    public class RondesVM : BaseViewModel
    {
        private ObservableCollection<Ronde> _rondes;
        private Ronde _selection;

        // constructeur sans argument - ok
        public RondesVM()
        {
            //
            _rondes = new ObservableCollection<Ronde>(App.Entities().Rondes);
            // ... autres collections si nécessaire
            if (_rondes.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _rondes[0];
            }
        }

        public ObservableCollection<Ronde> LesRondes
        {
            get { return _rondes; }
            set
            {
                _rondes.Clear();
                _rondes = value;
                FirePropertyChanged("LesRondes");
            }
        }

        public Ronde LaRonde
        {
            get
            {
                return _selection;
            }
            set
            {
                if(!(_selection == value))
                {
//
                    _selection = value;
                    FirePropertyChanged("LaRonde");
                }
            }
        }

        // public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void SuppressionSelection()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Rondes.Remove(LaRonde);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesRondes.Remove(LaRonde);
                _selection = null; // LaRonde = null;
                FirePropertyChanged("LaRonde");
                FirePropertyChanged("LesRondes");
            }
        }

        // public ICommand NouvelleRonde { get { return new ExecutableCommand(Ajout); } }
        public void AjoutNouvelleRonde()
        {
            // AnnuleImport();    //   <<<<<<<<<<<<<<--------
            Ronde nouvelleRonde = new Ronde();
            nouvelleRonde.Date = DateTime.Now;
            //
            // nouvelleRonde.Id  // Entity gère ceci ! 
            //
            // nouveauMeuble.TypeMeubleId = 
            App.Entities().Rondes.Add(nouvelleRonde);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesRondes.Add(nouvelleRonde);
            LaRonde = nouvelleRonde; // met à jour la sélection 
            FirePropertyChanged("LesRondes");
            FirePropertyChanged("LaRonde");
        }

        //public ICommand MajSelection { get { return new ExecutableCommand(MiseAJourSelection); } }
        //public void MiseAJourSelection()
        //{
        //}

        //public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
