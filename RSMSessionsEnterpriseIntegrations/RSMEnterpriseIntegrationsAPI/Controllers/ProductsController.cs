namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService service) : ControllerBase
    {
        private readonly IProductService _service = service;

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetProductById(id);
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            return Ok(await _service.CreateProduct(createProductDto));
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            return Ok(await _service.UpdateProduct(updateProductDto));
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProduct(id));
        }

    }
}
