using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Adapters.IAdapters;
using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.Domain.Services.IServices;
using ScheduleAppApi.DTOs.User;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;

namespace ScheduleAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepo;
        private readonly IAdapter _adapter;
        public RegisterController(
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
        [HttpPost("/api/Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest req)
        {
            if (await _userRepo.IsRegisteredAsync(req.Email))
                return BadRequest("User already exists");

            var password = _passwordService.CreatePasswordHash(req.Password);

            var user = new User
            {
                Email = req.Email,
                Role = ERole.User,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt,
            };

            var id = await _userRepo.RegisterAsync(user);

            return Created(nameof(Register), id);
        }
    }
}
