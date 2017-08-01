namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ENTRADA")]
    public partial class ENTRADA
    {
        [Key]
        public int cod_entrada { get; set; }

        public int cod_tipo_entrada { get; set; }

        public decimal? valor { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data da Entrada")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]  
        public DateTime data_entrada { get; set; }

        [StringLength(500)]
        public string observao { get; set; }

        public virtual TIPO_ENTRADA TIPO_ENTRADA { get; set; }
    }
}
