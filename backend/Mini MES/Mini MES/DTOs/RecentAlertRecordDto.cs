namespace MiniMES.DTOs
{
    public class RecentAlertRecordDto
    {
        public int Id { get; set; }
        public string EquipmentCode { get; set; } = string.Empty;
        public string AlertMessage { get; set; } = string.Empty;
        public DateTime AlertTime { get; set; }
    }
}