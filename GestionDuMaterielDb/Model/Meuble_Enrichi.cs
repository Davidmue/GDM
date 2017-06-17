using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMaterielDb.Model
{
    public partial class Meuble
    {

        public bool EstVacant
        {
            get
            {
                return (Employe == null); 
            }
            set
            {
                if (value) Employe = null;
            }
        }

    }
}
