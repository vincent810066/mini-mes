using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMES.Models
{
    [Table("equipment")]
    public class Equipment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("equipment_code")]
        public string EquipmentCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("equipment_name")]
        public string EquipmentName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("current_status")]
        public string CurrentStatus { get; set; } = "Normal";

        [Column("last_update_time")]
        public DateTime? LastUpdateTime { get; set; }
    }
}