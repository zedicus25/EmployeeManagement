
using Server.Model;
using Server.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Server.Controller
{
    internal class UserController
    {
        public List<User> Users { get; private set; }
        private JsonDataController<User> _jsonDataController;

        public UserController()
        {
            _jsonDataController = new JsonDataController<User>("data","users.txt");
            Users = _jsonDataController.ReadData();
        }

        public string CheckData(string data)
        {
            data = data.Trim();
            string[] datas = data.Split('\n');
            if (datas.Length != 3)
                return "false";
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i] = datas[i].Trim();
            }

            if (datas[1].ToLower().Contains("login=") && datas[2].ToLower().Contains("password="))
            {
                string login = datas[1].Substring(datas[1].IndexOf('=')+1).Trim();
                string password = datas[2].Substring(datas[2].IndexOf('=')+1).Trim();
                User user = Users.FirstOrDefault(u => u.Password.Equals(password) && u.Login.Equals(login));
                if (user != null)
                    return "true";
            }
            return "false";
        }
    }
}
