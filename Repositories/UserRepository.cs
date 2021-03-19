using System.Threading.Tasks;
using System.Linq;
using bouncer.Models;
using Microsoft.Extensions.Logging;
using System;

namespace bouncer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ILogger<UserRepository> _logger;
        public BouncerContext _context;
        public UserRepository(BouncerContext context, ILogger<UserRepository> logger) : base(context) {
            _logger = logger;
        }

        /// <summary>
        /// For User specifically, Email has to be Unique.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async new Task<User> Add(User user) {
            try {
                User existing = FindByEmail(user.Email);
                if (existing == null) {
                    return (await _context.AddAsync<User>(user)).Entity;
                } else {
                    return null;
                }
            } catch (Exception ex) {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public User FindByEmail(string email)
        {
            try {
                User user = _context.Users.FirstOrDefault<User>((user) => user.Email.Equals(email.ToLower()));
                return user;
            } catch (Exception ex) {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}