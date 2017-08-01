namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SEXO")]
    public partial class SEXO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SEXO()
        {
            DISCIPULO = new HashSet<DISCIPULO>();
        }

        [Key]
        public int ID_SEXO { get; set; }

        [Required]
        [StringLength(30)]
        public string NOME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCIPULO> DISCIPULO { get; set; }
    }
}
