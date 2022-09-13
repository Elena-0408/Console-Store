
namespace Store.Service
{
    public interface IRegisteredUserServices
    {
        public bool SearchGoodsByName(string name);
        public bool GetListOfGoods();
        public bool CreateNewOrder();
        public bool Cancellation(int id);
        public bool SignOut();
        public bool ChangeStatus(int id);
        public bool GetOrdersHistory();
        public bool ChangePersonalInformation(string userEmail, string userPassword, string userPhone);
       
    }
}
   

