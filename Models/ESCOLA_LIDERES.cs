namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ESCOLA_LIDERES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESCOLA_LIDERES()
        {
            DISCIPULO = new HashSet<DISCIPULO>();
        }

        [Key]
        public int ID_ESCOLA_LIDERES { get; set; }

        [Required]
        [StringLength(30)]
        public string OPCAO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCIPULO> DISCIPULO { get; set; }
    }
}
