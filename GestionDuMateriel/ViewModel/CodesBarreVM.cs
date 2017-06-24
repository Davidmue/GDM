using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;
using System.Windows.Input;


namespace GestionDuMateriel.ViewModel
{
    public class CodesBarreVM : BaseViewModel
    {
        private ObservableCollection<CodeBarre> _codesbarre;
        private CodeBarre _selection;

        private RondesVM _rondesVmDependance; 

        // constructeur OK - un code-barre a été scanné lors de la ronde en cours ... 
        public CodesBarreVM()
        {
            // RondesVM RondesVmDependance
            // _rondesVmDependance = RondesVmDependance;
            //
            ObservableCollection<CodeBarre> _codesbarre = new ObservableCollection<CodeBarre>(App.Entities().CodeBarres);
            if (_codesbarre.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _codesbarre[0];
            }
        }

        public ObservableCollection<CodeBarre> LesCodesBarre
        {
            get { return _codesbarre; }
            set
            {
                _codesbarre.Clear();
                _codesbarre = value;
                FirePropertyChanged("LesCodesBarre");
            }
        }

        public CodeBarre LeCodeBarre
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LeCodeBarre");
            }
        }

        public Ronde LaRonde
        {
            get
            {
                return _rondesVmDependance.LaRonde;
            }
        }

        // public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().CodeBarres.Remove(LeCodeBarre);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                _codesbarre.Remove(LeCodeBarre); // objets internes ici !
                _selection = null;
                FirePropertyChanged("LeCodeBarre");
                FirePropertyChanged("LesCodesBarre");
            }
        }


        // public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {

            int i = _codesbarre.Count;

            ////
            //// selection pour la ronde sélectionnée ... 
            //Ronde rondeSelectionnee;
            //if (_rondesVmDependance.LaRonde == null)
            //{
            //    if (_rondesVmDependance.LesRondes.Count == 0)
            //    {
            //        _rondesVmDependance.AjoutNouvelleRonde();
            //        rondeSelectionnee = _rondesVmDependance.LaRonde;
            //    }
            //    else
            //    {
            //        rondeSelectionnee = _rondesVmDependance.LesRondes[0];
            //    }
            //}
            //else
            //{
            //    rondeSelectionnee = _rondesVmDependance.LaRonde;
            //}
            //int rondeId = rondeSelectionnee.Id;


            CodeBarre nouveauCodeBarre = new CodeBarre();
            nouveauCodeBarre.Ignore = true;
            nouveauCodeBarre.Interpretation = "Pas de correspondance trouvée ! ";
            nouveauCodeBarre.NoDeSerie = "";
            if(_rondesVmDependance.LesRondes.Count == 0)
            {
                _rondesVmDependance.AjoutNouvelleRonde(); 
            }
            if(_rondesVmDependance.LaRonde == null)
            {
                nouveauCodeBarre.Ronde = _rondesVmDependance.LesRondes[0]; 
            }
            else
            {
                nouveauCodeBarre.Ronde = _rondesVmDependance.LaRonde;
            }
            //
            // nouveauCodeBarre.Id  // Entity gère ceci ! 
            //
            App.Entities().CodeBarres.Add(nouveauCodeBarre);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            _codesbarre.Add(nouveauCodeBarre);                                   // buggy  :-(
            _selection = nouveauCodeBarre; // met à jour la sélection 
            FirePropertyChanged("LesCodesBarre");
            FirePropertyChanged("LeCodeBarre");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
