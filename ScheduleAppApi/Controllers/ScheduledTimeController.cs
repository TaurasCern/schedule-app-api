using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Adapters.IAdapters;
using ScheduleAppApi.Domain.Services.IServices;
using ScheduleAppApi.DTOs.ScheduledTime;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;

namespace ScheduleAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledTimeController : ControllerBase
    {
        private readonly IScheduledTimeRepository _schTimeRepo;
        private readonly IAdapter _adapter;
        private readonly IHttpContextAccessor _contextAccessor;
        public ScheduledTimeController(
            IScheduledTimeRepository schTimeRepo,
            IAdapter adapter,
            IHttpContextAccessor contextAccessor)
        {
            _schTimeRepo = schTimeRepo;
            _adapter = adapter;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(ScheduledTimeRequest req)
        {
            if (!int.TryParse(_contextAccessor.HttpContext.User.Identity.Name, out int userId))
            {
                return BadRequest();
            }

            var schTime = _adapter.Bind(req, userId);

            await _schTimeRepo.CreateAsync(schTime);

            return Ok(_adapter.Bind(schTime));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (!int.TryParse(_contextAccessor.HttpContext.User.Identity.Name, out int userId))
            {
                return BadRequest();
            }

            var schTimes = await _schTimeRepo.GetAllAsync(st => st.UserId == userId);

            var response = _adapter.Bind(schTimes);

            return Ok(response);
        }
        [HttpDelete("{noteId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int noteId)
        {
            if (!int.TryParse(_contextAccessor.HttpContext.User.Identity.Name, out int userId))
            {
                return BadRequest();
            }

            var schTime = await _schTimeRepo.GetAsync(st => st.Id == noteId);

            if (schTime == null)
            {
                return NotFound();
            }

            if (schTime.UserId != userId)
            {
                return Unauthorized();
            }

            await _schTimeRepo.RemoveAsync(schTime);

            return Ok();
        }
        [HttpPut("{noteId:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int noteId, ScheduledTimeRequest request)
        {
            if (!int.TryParse(_contextAccessor.HttpContext.User.Identity.Name, out int userId))
            {
                return BadRequest();
            }

            var schTime = await _schTimeRepo.GetAsync(st => st.Id == noteId);

            if(schTime == null)
            {
                return NotFound();
            }

            if (schTime.UserId != userId)
            {
                return Unauthorized();
            }

            schTime.From = request.From;
            schTime.To = request.To;
            schTime.Note = request.Note;

            var updated = await _schTimeRepo.UpdateAsync(schTime);

            var response = _adapter.Bind(updated);

            return Ok(response);
        }
    }
}
