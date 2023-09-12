using BinBetter.Api.Data.Repositories;

namespace BinBetter.Api.Data
{
    public class BinBetterRepository : IBinBetterRepository
    {
        private BinBetterContext _context;
        private UserRepository _userRepository;
        private BinsRepository _binsRepository;
        private GoalsRepository _goalsRepository;

        public BinBetterRepository(BinBetterContext context)
        {
            _context = context;
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IGoalsRepository Goals
        {
            get
            {
                if (_goalsRepository == null)
                {
                    _goalsRepository = new GoalsRepository(_context);
                }
                return _goalsRepository;
            }
        }

        public IBinsRepository Bins
        {
            get
            {
                if (_binsRepository == null)
                {
                    _binsRepository = new BinsRepository(_context);
                }
                return _binsRepository;
            }
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
