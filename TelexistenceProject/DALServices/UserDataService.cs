using DAL.DataAccess;
using DAL.DataAccesss;
using DAL.Models;

namespace TelexistenceProject.DALServices
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataAccess _userDataAccess;
        public UserDataService(IUserDataAccess userDataAccess) {
            _userDataAccess = userDataAccess;
        }
        public async Task<UserReply> AddUser(UserModelRequest user)
        {
            var newUser = new UserModel()
            {
                FirstName = user.FirstName,
                SecondName = user.SecondName
            };
            var insertedUser = await _userDataAccess.CreateUser(newUser);
            return new UserReply()
            {
                FirstName = insertedUser.FirstName,
                SecondName = insertedUser.SecondName,
                UserId = insertedUser.Id,
            };
        }

        public async Task<UsersReply> DeleteUserById(string id)
        {
            await _userDataAccess.DeleteUser(new UserModel { Id = id});
            
            return await GetUsers();
        }

        public async Task<UserReply> GetUserById(string id)
        {
            var result = await _userDataAccess.GetUser(new UserModel { Id = id });
            return new UserReply()
            {
                FirstName = result.FirstName,
                SecondName = result.SecondName,
                UserId = result.Id
            };
        }

        public async Task<UsersReply> GetUsers()
        {
            var result = new UsersReply();
            var allUsers = await _userDataAccess.GetAllUsers();
            allUsers.ForEach(x =>
            {
                result.Users.Add(new UserReply()
                {
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    UserId = x.Id
                });
            });
            return result;
        }

        public async Task<UsersReply> UpdateUser(UserModelRequest user)
        {
            var userToInsert = new UserModel
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
            };
            await _userDataAccess.UpdateUser(userToInsert);
            return await GetUsers();
        }
    }
}
