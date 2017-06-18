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
    public class PiecesVM : BaseViewModel
    {

        private ObservableCollection<Piece> _pieces;
        private Piece _selection;
        private PlansVM _plansVMDependance;

        public PiecesVM(PlansVM PlansVmDependance)
        {
            //
            // Piece utilise TypePiece ... 
            _plansVMDependance = PlansVmDependance;
            //
            _pieces = new ObservableCollection<Piece>(App.Entities().Pieces);
            // ... autres collections si nécessaire
            if (_pieces.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _pieces[0];
            }
        }

        public ObservableCollection<Plan> LesPlans
        {
            get
            {
                return _plansVMDependance.Plans;
            }
        }

        public ObservableCollection<Piece> LesPieces
        {
            get { return _pieces; }
            set
            {
                _pieces.Clear();
                _pieces = value;
                FirePropertyChanged("LesPieces");
            }
        }

        public Piece LaPiece
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LaPiece");
            }
        }

        public ICommand Supprime { get { return new ExecutableCommand(Suppression); } }
        public void Suppression()
        {
            if (!(_selection == null))
            {
                // applique la suppression à la base de données ... 
                App.Entities().Pieces.Remove(LaPiece);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesPieces.Remove(LaPiece);
                _selection = null;
                FirePropertyChanged("LaPiece");
                FirePropertyChanged("LesPieces");
            }
        }


        public ICommand Ajoute { get { return new ExecutableCommand(Ajout); } }
        public void Ajout()
        {
            if (!(_plansVMDependance.Plans.Count == 0)) 
            {
                Piece nouvellePiece = new Piece(); 
                nouvellePiece.Description = "";
                Plan plan; 
                if (_plansVMDependance.Selection == null)
                {
                    plan = _plansVMDependance.Plans[0];
                }
                else
                {
                    plan = _plansVMDependance.Selection; 
                }
                nouvellePiece.Plan = plan;
                //
                // nouvellePiece.Id  // Entity gère ceci ! 
                //
                App.Entities().Pieces.Add(nouvellePiece);
                App.Entities().SaveChanges();
                // applique la suppression aux objets représentant les données ... 
                LesPieces.Add(nouvellePiece);
                _selection = nouvellePiece;
                FirePropertyChanged("LesPieces");
                FirePropertyChanged("LaPiece");
            }
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

    }

}
