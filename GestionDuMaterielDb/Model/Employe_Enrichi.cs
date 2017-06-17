using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMaterielDb.Model
{
    public partial class Employe
    {

        public string Description
        {
            get
            {
                return (Prenom.Trim() + " " + Nom.Trim()).Trim(); 
            }
        }

    }
}
