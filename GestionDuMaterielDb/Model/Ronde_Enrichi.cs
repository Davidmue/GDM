using System;
using System.Collections.Generic;
using System.IO;
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
                        result = ImportationNomFichier;
                    }
                    else
                    {
                        // dossier et fichier sont nulls ! 
                        result = "";
                    }
                }
                return result; 
            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    ImportationCheminDossierSource = "";
                    ImportationNomFichier = ""; 
                }
                else
                {
                    if (File.Exists(value))
                    {
                        ImportationCheminDossierSource = Path.GetDirectoryName(value);
                        ImportationNomFichier = Path.GetFileName(value); 
                    }
                    else
                    {
                        ImportationCheminDossierSource = "";
                        ImportationNomFichier = value; 
                    }
                }                
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
