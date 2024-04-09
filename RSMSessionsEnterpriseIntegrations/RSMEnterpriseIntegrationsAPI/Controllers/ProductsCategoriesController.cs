namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController(IProductCategoryService service) : ControllerBase
    {
        private readonly IProductCategoryService _service = service;

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetProductCategoryById (id);
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateProductCategoryDto createProductCategoryDto)
        {
            return Ok(await _service.CreateProductCategory (createProductCategoryDto));
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdateProductCategoryDto updateProductCategoryDto)
        {
            return Ok(await _service.UpdateProductCategory (updateProductCategoryDto ));
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProductCategory (id));
        }

    }
}
