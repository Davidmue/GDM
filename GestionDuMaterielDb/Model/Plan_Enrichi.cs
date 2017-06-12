using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMaterielDb.Model
{
    public partial class Plan
    {

        public string CheminCompletDuFichier
        {
            get
            {
                return System.IO.Path.Combine(CheminDossierSource.Trim(), NomFichier.Trim());
            }
        }

    }
}
