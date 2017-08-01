namespace SistemaIgreja.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAIDA")]
    public partial class SAIDA
    {
        [Key]
        public int cod_saida { get; set; }

        public int id_tipo_saida { get; set; }


        
        public decimal? valor { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data da Saída")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime data_saida { get; set; }

        [StringLength(500)]
        public string observacao { get; set; }

        public virtual TIPO_SAIDA TIPO_SAIDA { get; set; }
    }
}
