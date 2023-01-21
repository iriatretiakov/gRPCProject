using Grpc.Core;
using TelexistenceProject;
using TelexistenceProject.DALServices;

namespace TelexistenceProject.Services
{
    public class UserService: UsersData.UsersDataBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserDataService _userDataService;
        public UserService(ILogger<UserService> logger, IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        public override async Task<UserReply> GetUser(UserIdRequest request, ServerCallContext context)
        {
            return await _userDataService.GetUserById(request.UserId);
        }

        public override async Task<UsersReply> GetUsers(Ping request, ServerCallContext context)
        {
            return await _userDataService.GetUsers();
        }

        public override async Task<UsersReply> UpdateUser(UserModelRequest request, ServerCallContext context)
        {
            return await _userDataService.UpdateUser(request);
        }

        public override async Task<UserReply> CreateUser(UserModelRequest request, ServerCallContext context)
        {
            return await _userDataService.AddUser(request);
        }

        public override async Task<UsersReply> DeleteUser(UserIdRequest request, ServerCallContext context)
        {
            return await _userDataService.DeleteUserById(request.UserId);
        }
    }
}
