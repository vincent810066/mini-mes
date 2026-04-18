using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMES.Models
{
    [Table("production_record")]
    public class ProductionRecord
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("equipment_code")]
        public string EquipmentCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("work_order_no")]
        public string WorkOrderNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("serial_no")]
        public string SerialNo { get; set; } = string.Empty;

        [Column("voltage", TypeName = "numeric(10,2)")]
        public decimal Voltage { get; set; }

        [Column("temperature", TypeName = "numeric(10,2)")]
        public decimal Temperature { get; set; }

        [Column("record_time")]
        public DateTime RecordTime { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("result_status")]
        public string ResultStatus { get; set; } = "Normal";
    }
}