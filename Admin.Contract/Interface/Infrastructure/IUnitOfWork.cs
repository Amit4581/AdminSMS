using Admin.Contract.Interface.Implementation;

namespace Admin.Contract.Interface.Infrastructure
{
    public interface IUnitOfWork
    {
        //IMasterRepository MasterData { get; }
        IUserRepositry UserData { get; }
    }
}
