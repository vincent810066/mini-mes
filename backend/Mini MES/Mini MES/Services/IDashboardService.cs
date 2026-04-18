using MiniMES.DTOs;

//Service 介面

namespace MiniMES.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
        Task<List<EquipmentStatusDto>> GetEquipmentStatusAsync();
        Task<List<RecentProductionRecordDto>> GetRecentRecordsAsync();
        Task<List<RecentAlertRecordDto>> GetRecentAlertsAsync();
    }
}