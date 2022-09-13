using System;

namespace Store.Service
{
    public interface IGuestServices
    {
        public bool SearchGoodsByName(string name);
        public bool GetListOfGoods();
        public bool SignUp();
        public byte CheckLogin(string login, string password);
        public Tuple<string, string, byte> SignIn();
       
    }
}
