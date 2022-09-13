
namespace Store.Service
{
   public interface IUnitOfWork
    {
        IAdministratorServices AdminRepository { get; }
        IRegisteredUserServices RegisteredUserRepository { get; }
        IGuestServices GuestRepository { get; }

        
    }
}
