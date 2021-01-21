using SimpleAPI2.DBAccess.Authentication;
using System;

namespace SimpleAPI2.Models
{
    public class UserMasterRepository
    {
        public object ValidateUser(string username, string password)
        {
            User user = new User();
            return user.ValidateUserCredentials(username,password);
        }

    }
}