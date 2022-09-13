
namespace Store.StoreMenu
{
   public interface IMenuOfStore
    {
        public bool RoleDefinition();
        public void Start();
        public void GuestActions(string action);
        public void RegisteredUserActions(string action);
        public void AdministratorActions(string action);
        public void RolesActions(string role);
    }
}
