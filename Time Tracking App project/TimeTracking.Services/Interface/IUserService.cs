using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.Services.Interface
{
    public interface IUserService
    {
        ActiveUser Register(string firstName, string lastName, string username, string password, string email);
        bool ValidLogin(string username, string password);
        ActiveUser Login(string username, string password);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        ActiveUser ChangeFirstName(string username, string password, string newFirstName);
        ActiveUser ChangeLastName(string username, string password, string newLastName);
        bool DeactivateAccount(string username, string password); 
        bool ActivateAccount(string username, string password); 
    }
}
