namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RSMEnterpriseIntegrationsAPI.Application.DTOs;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController(IUserLoginService service) : ControllerBase
    {
        private readonly IUserLoginService _service = service;

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetUserLoginById(id);
            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CreateUserLoginDto createUserLoginDto)
        {
            return Ok(await _service.CreateUserLogin(createUserLoginDto));
        }


        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteUserLogin(id));
        }

    

    }
}
