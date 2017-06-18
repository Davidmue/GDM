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


namespace GestionDuMateriel.ViewModel
{
    public class RondesVM : BaseViewModel
    {
        private ObservableCollection<Ronde> _rondes;
        private Ronde _selection;

        private bool _annulationImportEnCours = false; 

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
                    if(!(_annulationImportEnCours))
                    {
                        AnnuleImport();
                    }
                    _selection = value;
                    FirePropertyChanged("LaRonde");
                }
            }
        }

        public Visibility AnnulerImportVisibility
        {
            get
            {
                Visibility result; 
                if(_selection == null)
                {
                    result = Visibility.Collapsed;
                }
                else
                {
                    if(_selection.ImportationFaite)
                    {
                        result = Visibility.Collapsed; 
                    }
                    else
                    {
                        result = Visibility.Visible; 
                    }
                }
                return result;
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
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


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            AnnuleImport();    //   <<<<<<<<<<<<<<--------
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
            _selection = nouvelleRonde; // met à jour la sélection 
            FirePropertyChanged("LesRondes");
            FirePropertyChanged("LaRonde");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

        public ICommand NouvelleImportation { get { return new ExecutableCommand(DemarreImport); } }
        public void DemarreImport()
        {
            Ajout();
            FirePropertyChanged("AnnulerImportVisibility");
        }

        public ICommand AnnulationImportation { get { return new ExecutableCommand(AnnuleImport); } }
        public void AnnuleImport()
        {
            if(!(_selection == null))
            {
                if(!(_selection.ImportationFaite))
                {
                    _annulationImportEnCours = true; 
                    Suppression();
                    _annulationImportEnCours = false; 
                }
                FirePropertyChanged("AnnulerImportVisibility");
            }
        }

    }

}
