namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIA_SEMANA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIA_SEMANA()
        {
            CELULA = new HashSet<CELULA>();
        }

        [Key]
        public int ID_DIA { get; set; }

        [Required]
        [StringLength(30)]
        public string NOME_DIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CELULA> CELULA { get; set; }
    }
}
