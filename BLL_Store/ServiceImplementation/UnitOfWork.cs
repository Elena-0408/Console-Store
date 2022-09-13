using Store.Service;
using Moq;
using Store.ServiceImplementation;

namespace Store.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataStore data;
        private readonly Mock<IDataStore> mock;
        private readonly IDataStore context;
        IAdministratorServices administratorRepository;
        IRegisteredUserServices registeredUserRepository;
        IGuestServices guestRepository;

        public UnitOfWork()
        {
            data = new DataStore();
            mock = new Mock<IDataStore>();
            
            mock.Setup(m => m.Administrators).Returns(data.Administrators);
            mock.Setup(m => m.RegisteredUsers).Returns(data.RegisteredUsers);
            mock.Setup(m => m.Products).Returns(data.Products);
            mock.Setup(m => m.Orders).Returns(data.Orders);
            context = mock.Object;

        }

        public IAdministratorServices AdminRepository
        {
            get
            {
                if (administratorRepository == null)
                {
                    administratorRepository = new AdministratorServices(context);
                }
                return administratorRepository;
            }
        }

        public IRegisteredUserServices RegisteredUserRepository
        {
            get
            {
                if (registeredUserRepository == null)
                {
                    registeredUserRepository = new RegisteredUserServices(context);
                }
                return registeredUserRepository;
            }
        }

        public IGuestServices GuestRepository
        {
            get
            {
                if (guestRepository == null)
                {
                    guestRepository = new GuestServices(context);
                }
                return guestRepository;
            }
        }
    }
}
