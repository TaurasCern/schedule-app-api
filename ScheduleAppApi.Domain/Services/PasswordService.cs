using ScheduleAppApi.Domain.DTOs;
using ScheduleAppApi.Domain.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace ScheduleAppApi.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Creates Hash and Salt for the given password
        /// </summary>
        /// <param name="password">password of the user</param>
        /// <returns>Password object containing Hash and Salt</returns>
        public Password CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA256();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return new() { Hash = passwordHash, Salt = passwordSalt };
        }
        /// <summary>
        /// Checks if the password is equal to the hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns>True if passord is valid</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
