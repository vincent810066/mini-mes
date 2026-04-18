using MiniMES.DTOs;

namespace MiniMES.Services
{
    public interface IProductionService
    {
        Task<(bool Success, string Message, object? Data)> CreateProductionRecordAsync(ProductionRecordRequest request);
    }
}