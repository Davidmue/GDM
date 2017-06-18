using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMaterielDb.Model
{
    public partial class Ronde
    {

        public string ImportationCheminCompletFichier
        {
            get
            {
                string result = "";
                if(!(string.IsNullOrEmpty(ImportationCheminDossierSource)))
                {
                    if (!(string.IsNullOrEmpty(ImportationNomFichier)))
                    {
                        result = System.IO.Path.Combine(ImportationCheminDossierSource, ImportationNomFichier);
                    }
                    else
                    {
                        // fichier est null ! 
                        result = ImportationNomFichier;
                    }
                }
                else
                {
                    // dossier est null ! 
                    if (!(string.IsNullOrEmpty(ImportationNomFichier)))
                    {
                        result = ImportationCheminDossierSource;
                    }
                    else
                    {
                        // dossier et fichier sont nulls ! 
                        result = "";
                    }
                }
                return result; 
            }
        }

        public bool ImportationFaite
        {
            get
            {
                return (!(ImportationDate == null)); 
            }
        }

    }
}
