using BusinessLogic;
using MagicDishWebApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicDishWebApplication.Repository
{
    public interface IUserAccountRepository
    {
        Task<List<UserAccount>> GetAsync();
    }
}
