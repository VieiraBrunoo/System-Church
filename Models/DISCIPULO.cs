namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DISCIPULO")]
    public partial class DISCIPULO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCIPULO()
        {
            CELULA = new HashSet<CELULA>();
            DISCIPULO1 = new HashSet<DISCIPULO>();
        }

        [Key]
        public int ID_DISCIPULO { get; set; }

        [Required]
        [StringLength(200)]
        public string NOME { get; set; }

        public int IDADE { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATA_NASC { get; set; }

        [Required]
        [StringLength(70)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(200)]
        public string ENDERECO { get; set; }

        [StringLength(50)]
        public string TELEFONE { get; set; }

        public int SEXO { get; set; }

        public int ESCOLA_LIDERES { get; set; }

        public int BATISMO { get; set; }

        public int ENCONTRO { get; set; }

        public int? DISCIPULADOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CELULA> CELULA { get; set; }

        public virtual FEZ_BATISMO FEZ_BATISMO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCIPULO> DISCIPULO1 { get; set; }

        public virtual DISCIPULO DISCIPULO2 { get; set; }

        public virtual FEZ_ENCONTRO FEZ_ENCONTRO { get; set; }

        public virtual ESCOLA_LIDERES ESCOLA_LIDERES1 { get; set; }

        public virtual SEXO SEXO1 { get; set; }
    }
}
