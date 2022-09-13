using Store.Entities;

namespace Store.Service
{
    public interface IAdministratorServices
    {
        public bool SearchGoodsByName(string name);
        
        public bool GetListOfGoods();
       
        public bool CreateNewOrder();
       
        public bool SignOut();
       
        public bool AddProduct(string name, string category, decimal price, string description);
        
        public bool ChangeProductInformation(int productId, string name, string category, decimal price, string description);
       
        public bool ChangeStatus(int id, StatusOfOrder status);
        
        public bool GetInformationOfUser(int userId);
       
        public bool ChangeInformationOfUser(int userId, string userName, string userEmail, string userPassword, string userPhone);
    }
}
