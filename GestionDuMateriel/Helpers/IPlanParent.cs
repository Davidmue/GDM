using GestionDuMateriel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDuMateriel.Helpers
{
    interface IPlanParent
    {
        void RaffraichiGridView();
        void FormulaireEnfantFerme(AffichePlan ap);
    }
}
