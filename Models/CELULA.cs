namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CELULA")]
    public partial class CELULA
    {
        [Key]
        public int ID_CELULA { get; set; }

        public int ID_DISCIPULO { get; set; }

        [Required]
        [StringLength(50)]
        public string LUGAR { get; set; }

        public int DIA { get; set; }

        public int HORARIO { get; set; }

        public virtual DIA_SEMANA DIA_SEMANA { get; set; }

        public virtual DISCIPULO DISCIPULO { get; set; }

        public virtual HORARIO_CELULA HORARIO_CELULA { get; set; }
    }
}
