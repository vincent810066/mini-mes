using Microsoft.EntityFrameworkCore;
using MiniMES.Data;
using MiniMES.DTOs;

namespace MiniMES.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            var todayRecords = _context.ProductionRecords
                .Where(x => x.RecordTime >= today && x.RecordTime < tomorrow);

            var result = new DashboardSummaryDto
            {
                TodayTotal = await todayRecords.CountAsync(),
                TodayNormal = await todayRecords.CountAsync(x => x.ResultStatus == "Normal"),
                TodayAbnormal = await todayRecords.CountAsync(x => x.ResultStatus == "Abnormal")
            };

            return result;
        }

        public async Task<List<EquipmentStatusDto>> GetEquipmentStatusAsync()
        {
            var result = await _context.Equipments
                .OrderBy(x => x.EquipmentCode)
                .Select(x => new EquipmentStatusDto
                {
                    EquipmentCode = x.EquipmentCode,
                    EquipmentName = x.EquipmentName,
                    CurrentStatus = x.CurrentStatus,
                    LastUpdateTime = x.LastUpdateTime
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<RecentProductionRecordDto>> GetRecentRecordsAsync()
        {
            var result = await _context.ProductionRecords
                .OrderByDescending(x => x.RecordTime)
                .Take(10)
                .Select(x => new RecentProductionRecordDto
                {
                    Id = x.Id,
                    EquipmentCode = x.EquipmentCode,
                    WorkOrderNo = x.WorkOrderNo,
                    SerialNo = x.SerialNo,
                    Voltage = x.Voltage,
                    Temperature = x.Temperature,
                    RecordTime = x.RecordTime,
                    ResultStatus = x.ResultStatus
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<RecentAlertRecordDto>> GetRecentAlertsAsync()
        {
            var result = await _context.AlertRecords
                .OrderByDescending(x => x.AlertTime)
                .Take(10)
                .Select(x => new RecentAlertRecordDto
                {
                    Id = x.Id,
                    EquipmentCode = x.EquipmentCode,
                    AlertMessage = x.AlertMessage,
                    AlertTime = x.AlertTime
                })
                .ToListAsync();

            return result;
        }
    }
}