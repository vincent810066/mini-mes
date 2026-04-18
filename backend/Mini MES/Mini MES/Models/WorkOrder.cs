using System.ComponentModel.DataAnnotations;

namespace MiniMES.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string WorkOrderNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        public int PlannedQuantity { get; set; }

        public int ProducedQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}