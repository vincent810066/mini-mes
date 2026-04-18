using Microsoft.EntityFrameworkCore;
using MiniMES.Data;
using MiniMES.DTOs;
using MiniMES.Models;

namespace MiniMES.Services
{
    public class ProductionService : IProductionService
    {
        private readonly AppDbContext _context;

        public ProductionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, object? Data)> CreateProductionRecordAsync(ProductionRecordRequest request)
        {
            var equipment = await _context.Equipments
                .FirstOrDefaultAsync(x => x.EquipmentCode == request.EquipmentCode);

            if (equipment == null)
            {
                return (false, $"equipmentCode '{request.EquipmentCode}' does not exist.", null);
            }

            bool voltageAbnormal = request.Voltage < 210 || request.Voltage > 230;
            bool temperatureAbnormal = request.Temperature > 80;

            string resultStatus = (voltageAbnormal || temperatureAbnormal) ? "Abnormal" : "Normal";

            string alertMessage = string.Empty;

            if (voltageAbnormal && temperatureAbnormal)
            {
                alertMessage = $"Voltage abnormal ({request.Voltage}), Temperature abnormal ({request.Temperature})";
            }
            else if (voltageAbnormal)
            {
                alertMessage = $"Voltage abnormal ({request.Voltage})";
            }
            else if (temperatureAbnormal)
            {
                alertMessage = $"Temperature abnormal ({request.Temperature})";
            }

            var productionRecord = new ProductionRecord
            {
                EquipmentCode = request.EquipmentCode,
                WorkOrderNo = request.WorkOrderNo,
                SerialNo = request.SerialNo,
                Voltage = request.Voltage,
                Temperature = request.Temperature,
                RecordTime = DateTime.SpecifyKind(request.RecordTime, DateTimeKind.Utc),
                ResultStatus = resultStatus
            };

            _context.ProductionRecords.Add(productionRecord);

            if (resultStatus == "Abnormal")
            {
                var alertRecord = new AlertRecord
                {
                    EquipmentCode = request.EquipmentCode,
                    WorkOrderNo = request.WorkOrderNo,
                    SerialNo = request.SerialNo,
                    Voltage = request.Voltage,
                    Temperature = request.Temperature,
                    AlertMessage = alertMessage,
                    AlertTime = DateTime.SpecifyKind(request.RecordTime, DateTimeKind.Utc)
                };

                _context.AlertRecords.Add(alertRecord);
            }

            equipment.CurrentStatus = resultStatus;
            equipment.LastUpdateTime = DateTime.SpecifyKind(request.RecordTime, DateTimeKind.Utc);

            await _context.SaveChangesAsync();

            return (true, "Production record created successfully.", new
            {
                productionRecord.Id,
                productionRecord.EquipmentCode,
                productionRecord.WorkOrderNo,
                productionRecord.SerialNo,
                productionRecord.Voltage,
                productionRecord.Temperature,
                productionRecord.RecordTime,
                productionRecord.ResultStatus,
                AlertMessage = resultStatus == "Abnormal" ? alertMessage : null
            });
        }
    }
}