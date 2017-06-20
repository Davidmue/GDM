using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GestionDuMateriel.ViewModel
{
    public class TypesMaterielVM : BaseViewModel
    {
        private ObservableCollection<TypeMateriel> _typesMateriel;
        private TypeMateriel _selection; 

        public TypesMaterielVM()
        {
            _typesMateriel = new ObservableCollection<TypeMateriel>(App.Entities().TypeMateriels);
            if (_typesMateriel.Count == 0)
            {
                NouveauType();
            }
            else
            {
                _selection = _typesMateriel[0]; 
            }
        }

        public TypeMateriel TypeMaterielSelection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                FirePropertyChanged("TypeMaterielSelection");
            }
        }

        public ObservableCollection<TypeMateriel> TypesMateriel {
            get
            {
                return _typesMateriel; 
            }
            set
            {
                _typesMateriel = value;
                FirePropertyChanged("TypesMateriel");
            }
        }

        public ICommand NouveauTypeMateriel { get { return new ExecutableCommand(NouveauType); } }
        public void NouveauType()
        {
            _selection = new TypeMateriel();
            _selection.Description = "";
            App.Entities().TypeMateriels.Add(_selection);
            TypesMateriel.Add(_selection);
            FirePropertyChanged("TypesMateriel");
            FirePropertyChanged("TypeMaterielSelection");
        }

        public ICommand SuppressionTypeMateriel { get { return new ExecutableCommand(SuppressionType); } }
        public void SuppressionType()
        {
            if(!(_selection == null))
            {
                App.Entities().TypeMateriels.Remove(_selection);
                TypesMateriel.Remove(_selection);
                _selection = null;
                FirePropertyChanged("TypeMaterielSelection");
                FirePropertyChanged("TypesMateriel");
            }
        }

        public ICommand EnregistrerTypeMateriel { get { return new ExecutableCommand(EnregistrerType); } }
        public void EnregistrerType()
        {
            App.Entities().SaveChanges(); 
        }

    }
}
