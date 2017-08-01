namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TIPO_ENTRADA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_ENTRADA()
        {
            ENTRADA = new HashSet<ENTRADA>();
        }

        [Key]
        public int ID_TIPO_ENTRADA { get; set; }

        [Required]
        [StringLength(30)]
        public string TIPO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENTRADA> ENTRADA { get; set; }
    }
}
