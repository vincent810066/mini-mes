using Microsoft.AspNetCore.Mvc;
using MiniMES.Services;

namespace MiniMES.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _dashboardService.GetSummaryAsync();
            return Ok(result);
        }

        [HttpGet("equipment-status")]
        public async Task<IActionResult> GetEquipmentStatus()
        {
            var result = await _dashboardService.GetEquipmentStatusAsync();
            return Ok(result);
        }

        [HttpGet("recent-records")]
        public async Task<IActionResult> GetRecentRecords()
        {
            var result = await _dashboardService.GetRecentRecordsAsync();
            return Ok(result);
        }

        [HttpGet("recent-alerts")]
        public async Task<IActionResult> GetRecentAlerts()
        {
            var result = await _dashboardService.GetRecentAlertsAsync();
            return Ok(result);
        }
    }
}