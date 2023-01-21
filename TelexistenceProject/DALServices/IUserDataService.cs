namespace TelexistenceProject.DALServices
{
    public interface IUserDataService
    {
        Task<UserReply> GetUserById(string id);
        Task<UsersReply> GetUsers();
        Task<UsersReply> DeleteUserById(string id);
        Task<UsersReply> UpdateUser(UserModelRequest user);
        Task<UserReply> AddUser(UserModelRequest user);
    }
}
