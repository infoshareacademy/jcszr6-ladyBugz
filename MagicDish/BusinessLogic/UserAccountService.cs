﻿namespace BusinessLogic
{
    public class UserAccountService
    {
        public static List<UserAccount> Users = new List<UserAccount>
        {
            new UserAccount("Tomek","tomus123","tomek@gmail.com")
        };

        public static List<UserAccount> GetUsers()
        {
            return Users;
        }

        public static void AddUser(UserAccount user)
        {
            Users.Add(user);
        }
    }
}