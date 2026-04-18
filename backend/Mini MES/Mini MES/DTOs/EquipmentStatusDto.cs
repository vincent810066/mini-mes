namespace MiniMES.DTOs
{
    public class EquipmentStatusDto
    {
        public string EquipmentCode { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public DateTime? LastUpdateTime { get; set; }
    }
}