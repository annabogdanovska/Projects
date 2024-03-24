
using TimeTracking.Models.Enums;

namespace TimeTracking.Models
{
    public abstract class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public string Email { get; set; }

        public User(string firstName, string lastName, string username, string password, UserRole userRole, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            UserRole = userRole;
            Email = email;
        }
        public bool PasswordMatch(string password)
        {
            return Password == password;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public void ChangeLastName(string lastName)
        {
            LastName = lastName;
        }
    }
}
