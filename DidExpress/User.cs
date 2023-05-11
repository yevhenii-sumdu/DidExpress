using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidExpress {
    public class User {
        public string UserName { get; private set;  }
        public string UserLogin { get; private set; }
        public bool Login(string login, string password) {
            if (login == "elf" && password == "pass") {
                UserName = login;
                UserLogin = login;
                return true;
            }
            else {
                return false;
            }
        }
    }
}
