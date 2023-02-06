using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Adapters.IAdapters;
using ScheduleAppApi.Domain.Services.IServices;
using ScheduleAppApi.DTOs.User;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;

namespace ScheduleAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepo;
        private readonly IAdapter _adapter;
        public LoginController(
            IJwtService jwtService, 
            IPasswordService passwordService,
            IUserRepository userRepo,
            IAdapter adapter)
        {
            _jwtService = jwtService;
            _passwordService = passwordService;
            _userRepo = userRepo;
            _adapter = adapter;
        }
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody]LoginRequest req)
        {
            var result = await _userRepo.TryLoginAsync(req.Email, req.Password);

            if (!result.Key)
                return Unauthorized();

            var response = _adapter.Bind(result.Value);

            var token = _jwtService.GetJwtToken(result.Value.Id, response.Role);

            response.Token = token;

            return Ok(response);
        }
    }
}
