namespace Modulo_Administracion.Clases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_cliente_cuenta_corriente
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Id_factura { get; set; }

        public DateTime? Fecha { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Tipo_Factura { get; set; }

        public long? Nro_Factura { get; set; }

        public decimal? Imp_Factura { get; set; }

        public decimal? Pago_1 { get; set; }

        public DateTime? Fecha_Pago_1 { get; set; }

        public decimal? Pago_2 { get; set; }

        public DateTime? Fecha_Pago_2 { get; set; }

        public decimal? Pago_3 { get; set; }

        public DateTime? Fecha_Pago_3 { get; set; }

        public decimal? Pago_4 { get; set; }

        public DateTime? Fecha_Pago_4 { get; set; }

        public decimal? Saldo { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal S_Acum { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Condicion_Factura { get; set; }

        [StringLength(500)]
        public string Observacion_Factura { get; set; }

        [StringLength(500)]
        public string Observacion_Movimiento_Cta_Cte { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string Cliente_Nombre_Fantasia { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Cliente { get; set; }

        [StringLength(8000)]
        public string Cuit { get; set; }
    }
}
