//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionDuMaterielDb.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PresenceMateriel
    {
        public int Id { get; set; }
        public int RondeId { get; set; }
        public int MaterielId { get; set; }
        public int MeubleId { get; set; }
    
        public virtual Materiel Materiel { get; set; }
        public virtual Meuble Meuble { get; set; }
        public virtual Ronde Ronde { get; set; }
    }
}
