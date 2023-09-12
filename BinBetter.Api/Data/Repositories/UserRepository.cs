using BinBetter.Api.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BinBetter.Api.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BinBetterContext context) : base(context)
        {
        }

        public override IQueryable<User> QueryableAsync()
        {
            return base.QueryableAsync().AsNoTracking();
        }

        public IQueryable<User> FindByUsername(string username)
        {
            return _context.Users.Where(u => u.Username != null && u.Username.Equals(username));
        }

        public IQueryable<User> FindByEmail(string email)
        {
            return _context.Users.Where(u => u.Email != null && u.Email.Equals(email));
        }
    }
}
