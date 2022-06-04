using CST350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();

        public UserModel GetUser(UserModel user)
        {
            return securityDAO.FindUserByNameAndPassword(user);
        }

        public bool UserRegistered(UserModel user)
        {
            return securityDAO.CreateUser(user);
        }
    }
}
