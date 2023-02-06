using ScheduleAppApi.Domain.DTOs;

namespace ScheduleAppApi.Domain.Services.IServices
{
    public interface IPasswordService
    {
        Password CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
