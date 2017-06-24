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
    public class MeublesVM : BaseViewModel
    {
        private ObservableCollection<Meuble> _meubles;
        private Meuble _selection;
        private PiecesVM _piecesVmDependance;
        private EmployesVM _employesVmDependance;

        public MeublesVM(PiecesVM PiecesVmDependance, EmployesVM EmployesVmDependance)
        {
            //
            // Meuble utilise Piece et Employe ... 
            _piecesVmDependance = PiecesVmDependance;
            _employesVmDependance = EmployesVmDependance;
            //
            _meubles = new ObservableCollection<Meuble>(App.Entities().Meubles.OrderBy(m => m.Piece.Plan.Description).ThenBy(
                    m => m.Piece.Description).ThenBy(m => m.Description));
            // ... autres collections si nécessaire
            if (_meubles.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _meubles[0];
            }
        }

        public ObservableCollection<Piece> LesPieces
        {
            get
            {
                return _piecesVmDependance.LesPieces;
            }
        }

        public ObservableCollection<Employe> LesEmployes
        {
            get
            {
                return _employesVmDependance.LesEmployes; 
            }
        }

        public ObservableCollection<Meuble> LesMeubles
        {
            get { return _meubles; }
            set
            {
                _meubles.Clear();
                _meubles = value;
                FirePropertyChanged("LesMeubles");
            }
        }

        public Meuble LeMeuble
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LeMeuble");
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Meubles.Remove(LeMeuble);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesMeubles.Remove(LeMeuble);
                LeMeuble = null;
                FirePropertyChanged("LeMeuble");
                FirePropertyChanged("LesMeubles");
            }
        }


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            Meuble nouveauMeuble = new Meuble();
            nouveauMeuble.Description = ""; 
            nouveauMeuble.CodeBarre = "";
            //
            // nouveauMeuble.Id  // Entity gère ceci ! 
            //
            // nouveauMeuble.Employes = 
            // nouveauMeuble.Piece = 
            // nouveauMeuble.PresenceMateriels = 

            //
            // un type au minimum 1 élément doit être créé ...
            ObservableCollection<Piece> pieces = _piecesVmDependance.LesPieces;
            if (pieces.Count == 0)
            {
                _piecesVmDependance.Ajout(); 
            }
            Piece piece = _piecesVmDependance.LaPiece;
            nouveauMeuble.Piece = piece;

            //
            // un type au minimum 1 élément doit être créé ...
            ObservableCollection<Employe> employes = _employesVmDependance.LesEmployes;
            if (employes.Count == 0)
            {
                _employesVmDependance.Ajout();
            }
            // == PAS OBLIGATOIREMENT
            // Employe employe = _employesVmDependance.LEmploye;
            nouveauMeuble.Employe = null;

            //
            // nouveauMeuble.TypeMeubleId = 
            App.Entities().Meubles.Add(nouveauMeuble);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesMeubles.Add(nouveauMeuble);
            _selection = nouveauMeuble; // met à jour la sélection 
            FirePropertyChanged("LesMeubles");
            FirePropertyChanged("LeMeuble");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
