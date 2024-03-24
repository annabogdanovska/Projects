using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.Services.Helpers
{
    public static class UserHelper
    {
        public static bool IsActiveUser(this ActiveUser activeUser)
        {
            if(activeUser.UserRole != Models.Enums.UserRole.Active)
            {
                return false;
            }
            return true;
        }
    }
}
