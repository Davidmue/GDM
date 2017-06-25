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

        public CodesBarreVM(RondesVM RondesVmDependance)
        {
            // 
            // CodeBarre utilise Ronde ...
            _rondesVmDependance = RondesVmDependance;
            //
            _codesbarre = new ObservableCollection<CodeBarre>(
                App.Entities().CodeBarres.OrderBy(c => c.Ronde.Date).ThenBy(c => c.Id));
            // sélection par défaut ... 
            if (_codesbarre.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _codesbarre[0];
            }
        }

        // dépendance en lecture seule
        public ObservableCollection<Ronde> LesRondes
        {
            get
            {
                return _rondesVmDependance.LesRondes;
            }
        }

        // contenu géré en RW ... 
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
            get { return _selection; }
            set
            {
                _selection = value;
                FirePropertyChanged("LeCodeBarre");
            }
        }

        // commandes accessibles en WPF XAML ... 
        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().CodeBarres.Remove(LeCodeBarre);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesCodesBarre.Remove(LeCodeBarre); // TODO: objets internes ici ?!
                _selection = null;
                FirePropertyChanged("LeCodeBarre");
                FirePropertyChanged("LesCodesBarre");
            }
        }

        // Attention ! Pas d'ajout quand il n'y a aucun objet dans la table Ronde ! 
        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            if(!(_rondesVmDependance.LesRondes.Count == 0))
            {
                CodeBarre nouveauCodeBarre = new CodeBarre();
                nouveauCodeBarre.Ignore = true;
                nouveauCodeBarre.Interpretation = "Pas de correspondance trouvée ! ";
                nouveauCodeBarre.NoDeSerie = "";
                Ronde ronde;
                if (_rondesVmDependance.LaRonde == null)
                {
                    ronde = _rondesVmDependance.LesRondes[0];
                }
                else
                {
                    ronde = _rondesVmDependance.LaRonde;
                }
                nouveauCodeBarre.Ronde = ronde; 
                //
                // nouveauCodeBarre.Id = -1;   // Entity gère ceci ! 
                //
                App.Entities().CodeBarres.Add(nouveauCodeBarre);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesCodesBarre.Add(nouveauCodeBarre);                                   // buggy  :-(
                _selection = nouveauCodeBarre; // met à jour la sélection 
                FirePropertyChanged("LesCodesBarre");
                FirePropertyChanged("LeCodeBarre");
            }
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
