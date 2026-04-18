using Microsoft.AspNetCore.Mvc;
using MiniMES.DTOs;
using MiniMES.Services;

namespace MiniMES.Controllers
{
    [ApiController]
    [Route("api/production-record")]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionService _productionService;

        public ProductionController(IProductionService productionService)
        {
            _productionService = productionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductionRecord([FromBody] ProductionRecordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productionService.CreateProductionRecordAsync(request);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }
    }
}