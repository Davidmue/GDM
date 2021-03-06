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
    
    public partial class Meuble
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meuble()
        {
            this.PresenceMateriels = new HashSet<PresenceMateriel>();
        }
    
        public int Id { get; set; }
        public string CodeBarre { get; set; }
        public int PieceId { get; set; }
        public Nullable<int> EmployeId { get; set; }
        public string Description { get; set; }
    
        public virtual Piece Piece { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PresenceMateriel> PresenceMateriels { get; set; }
        public virtual Employe Employe { get; set; }
    }
}
