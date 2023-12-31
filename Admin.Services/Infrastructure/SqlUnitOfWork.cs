using Admin.Contract.Interface.Infrastructure;
using Admin.Contract.Interface.Implementation;

namespace Admin.Services.Repository.Infrastructure
{
    public class SqlUnitOfWork : IUnitOfWork
    {
       // private readonly IMasterRepository _masterRepository;
        private readonly IUserRepositry _userRepositry;
        public SqlUnitOfWork( IUserRepositry userRepositry)
        {
           // _masterRepository = masterRepository;
            _userRepositry = userRepositry;
        }
        //public IMasterRepository MasterData { get { return _masterRepository; } }
        public IUserRepositry UserData { get { return _userRepositry; } }
    }
}
