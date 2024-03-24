using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Services.Interface;
using TimeTracking.Models;
using TimeTracking.Models.Database;
using TimeTracking.Services.Helpers;

namespace TimeTracking.Services
{
    public class UserService : IUserService
    {
        public IDatabase<ActiveUser> _userDatabase;

        public UserService(IDatabase<ActiveUser> userDatabase)
        {
            _userDatabase = userDatabase;
        }

        public ActiveUser Register(string firstName, string lastName, string username, string password, string email)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (!ValidationHelper.ValidFirstLastName(firstName))
            {
                throw new ArgumentException("Invalid data for first name");
            }

            if (!ValidationHelper.ValidFirstLastName(lastName))
            {
                throw new ArgumentException("Invalid data for last name");
            }

            if (!ValidationHelper.ValidUserName(username))
            {
                throw new ArgumentException("Invalid data for username");
            }

            if (!ValidationHelper.ValidPassword(password))
            {
                throw new ArgumentException("Invalid password");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("Invalid email");
            }

            if(_userDatabase.GetAll().Any(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new Exception("User with that username exists");
            }

            Console.ResetColor();

            ActiveUser activeUser = new ActiveUser(firstName, lastName, username, password, email);
            _userDatabase.Insert(activeUser);

            return activeUser;
        }

        public bool ValidLogin(string username, string password)
        {

            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(password));

            if (u == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username or password is incorect");
                Console.ResetColor();
                return false;
            }

            return true;
        }

        public ActiveUser Login(string username, string password)
        {

            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(password));

            if (u == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username or password is incorect");
                Console.ResetColor();
            }

            return u;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(oldPassword));

            if (u == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("User not found");
                Console.ResetColor();
            }

            if (!ValidationHelper.ValidPassword(newPassword))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("New password does not meet the requirements");
                Console.ResetColor();
            }

            u.ChangePassword(newPassword);
            _userDatabase.Update(u);

            return true;
        }
        public ActiveUser ChangeFirstName(string username, string password, string newFirstName)
        {
            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(password));

            if (u == null)
            {
                throw new Exception("Username or password is incorect");
            }

            u.ChangeFirstName(newFirstName);
            _userDatabase.Update(u);

            return u;
        }

        public ActiveUser ChangeLastName(string username, string password, string newLastName)
        {
            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(password));

            if (u == null)
            {
                throw new Exception("Username or password is incorect");
            }
            u.ChangeLastName(newLastName);
            _userDatabase.Update(u);

            return u;
        }

        public bool DeactivateAccount(string username, string password)
        {
            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));

            if(u == null)
            {
                throw new Exception("Username or password is incorect");
            }

            u.DeactivateAccount();
            _userDatabase.Update(u);

            return true;
        }

        public bool ActivateAccount(string username, string password)
        {
            ActiveUser u = _userDatabase.GetAll().FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase) && x.PasswordMatch(password));
            if (u == null)
            {
                throw new Exception("Username or password is incorect");
            }

            u.ActivateAccount();
            _userDatabase.Update(u);

            return true;
        }
    }
}
