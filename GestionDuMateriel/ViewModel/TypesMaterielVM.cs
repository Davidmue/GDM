using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDuMaterielDb.Model;
using GestionDuMateriel.Helpers;
using System.Collections.ObjectModel;

namespace GestionDuMateriel.ViewModel
{
    public class TypesMaterielVM : BaseViewModel
    {
        private ObservableCollection<TypeMateriel> _typesMateriel;
        private GestionDuMaterielEntities _entities;

        public TypesMaterielVM()
        {
            _entities = new GestionDuMaterielEntities();
            TypesMateriel = new ObservableCollection<TypeMateriel>(_entities.TypeMateriels);
            if (TypesMateriel.Count == 0)
            {
                TypeMaterielSelection = new TypeMateriel(); 
            } else
            {
                TypeMaterielSelection = TypesMateriel[0]; 
            }
        }

        public TypeMateriel TypeMaterielSelection { get; set; }

        public ObservableCollection<TypeMateriel> TypesMateriel {
            get
            {
                return _typesMateriel; 
            }
            set
            {
                _typesMateriel = value;
                // raise ...
            }
        }

        public void Save()
        {
            _entities.SaveChanges(); 
        }

    }
}
