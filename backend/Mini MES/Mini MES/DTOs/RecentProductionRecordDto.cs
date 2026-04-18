namespace MiniMES.DTOs
{
    public class RecentProductionRecordDto
    {
        public int Id { get; set; }
        public string EquipmentCode { get; set; } = string.Empty;
        public string WorkOrderNo { get; set; } = string.Empty;
        public string SerialNo { get; set; } = string.Empty;
        public decimal Voltage { get; set; }
        public decimal Temperature { get; set; }
        public DateTime RecordTime { get; set; }
        public string ResultStatus { get; set; } = string.Empty;
    }
}