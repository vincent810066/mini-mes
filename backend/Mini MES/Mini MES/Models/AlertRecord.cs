using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMES.Data
{
    [Table("alert_record")]
    public class AlertRecord
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("equipment_code")]
        public string EquipmentCode { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("work_order_no")]
        public string? WorkOrderNo { get; set; }

        [MaxLength(50)]
        [Column("serial_no")]
        public string? SerialNo { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("alert_message")]
        public string AlertMessage { get; set; } = string.Empty;

        [Column("voltage")]
        public decimal? Voltage { get; set; }

        [Column("temperature")]
        public decimal? Temperature { get; set; }

        [Column("alert_time")]
        public DateTime AlertTime { get; set; }
    }
}