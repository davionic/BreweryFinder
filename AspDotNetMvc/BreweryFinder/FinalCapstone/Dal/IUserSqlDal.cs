using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public interface IUserSqlDal
    {
        bool NewBrewer(User user);
        bool DeleteBrewer(int userId);
        bool ValidateUser(User user);
        bool ValidateAdmin(User user);
    }
}
