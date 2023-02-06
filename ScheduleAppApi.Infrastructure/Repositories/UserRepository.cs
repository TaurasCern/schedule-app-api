using Microsoft.EntityFrameworkCore;
using ScheduleAppApi.Domain.Models;
using ScheduleAppApi.Domain.Services.IServices;
using ScheduleAppApi.Infrastructure.Database;
using ScheduleAppApi.Infrastructure.Repositories.IRepositories;

namespace ScheduleAppApi.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IRepository<User>, IUserRepository
    {
        private readonly ScheduleContext _db;
        private readonly IPasswordService _passwordService;
        public UserRepository(ScheduleContext db, IPasswordService passwordService)
            : base(db)
        {
            _db = db;
            _passwordService = passwordService;
        }
        /// <summary>
        /// Should return a flag indicating if a user with specified email already exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A flag indicating if email is already registed</returns>
        public async Task<bool> IsRegisteredAsync(string email)
            => await _db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        /// <summary>
        /// Checks if login request is valid and returns KeyValuePair with boolean if it was success and user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>Key is bool indicating if login was successful</returns>
        public async Task<KeyValuePair<bool, User?>> TryLoginAsync(string email, string password)
        {
            var user = await _db.Users
               .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
                return new(false, user);

            if (!_passwordService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new(false, user);

            return new(true, user);
        }
        /// <summary>
        /// Adds new user return id
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id</returns>
        public async Task<int> RegisterAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }
    }
}
