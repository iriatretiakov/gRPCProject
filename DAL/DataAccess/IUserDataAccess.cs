using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IUserDataAccess
    {
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUser(UserModel user);
        Task<UserModel> CreateUser(UserModel user);
        Task DeleteUser(UserModel user);
        Task UpdateUser(UserModel user);
    }
}
