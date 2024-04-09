namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderHeaderController(ISalesOrderHeaderService service) : ControllerBase
    {
        private readonly ISalesOrderHeaderService _service = service;

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetSalesOrderHeaderById (id);
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateSalesOrderHeaderDto createSalesOrderHeaderDto)
        {
            return Ok(await _service.CreateSalesOrderHeader(createSalesOrderHeaderDto));
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdateSalesOrderHeaderDto updateSalesOrderHeaderDto)
        {
            return Ok(await _service.UpdateSalesOrderHeader(updateSalesOrderHeaderDto));
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteSalesOrderHeader(id));
        }

    }
}
