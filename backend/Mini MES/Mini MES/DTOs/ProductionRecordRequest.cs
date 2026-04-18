using System.ComponentModel.DataAnnotations;

namespace MiniMES.DTOs
{
    public class ProductionRecordRequest
    {
        [Required]
        public string EquipmentCode { get; set; } = string.Empty;

        [Required]
        public string WorkOrderNo { get; set; } = string.Empty;

        [Required]
        public string SerialNo { get; set; } = string.Empty;

        [Required]
        public decimal Voltage { get; set; }

        [Required]
        public decimal Temperature { get; set; }

        [Required]
        public DateTime RecordTime { get; set; }
    }
}