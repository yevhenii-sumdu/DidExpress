﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidExpress {
    public class User {
        public bool Login(string login, string password) {
            if (login == "elf" && password == "pass") {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
