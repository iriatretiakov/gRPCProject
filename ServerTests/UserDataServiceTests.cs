using AutoFixture;
using DAL.DataAccess;
using DAL.Models;
using Moq;
using TelexistenceProject.DALServices;

namespace ServerTests
{
    public class Tests
    {
        Fixture fixture = new Fixture();
        IUserDataService userDataService;
        Mock<IUserDataAccess> userDataAccessMock;
        [SetUp]
        public void Setup()
        {
            userDataAccessMock = new Mock<IUserDataAccess>();
            userDataService = new UserDataService(userDataAccessMock.Object);
        }

        [Test]
        public async Task CheckGetUserById_success()
        {
            var mockUserModel = fixture.Create<UserModel>();
            userDataAccessMock.Setup(
                m => m.GetUser(It.IsAny<UserModel>())).Returns(Task.FromResult(mockUserModel));
            // Act
            var response = await userDataService.GetUserById(It.IsAny<string>());
            // Assert
            Assert.AreEqual(mockUserModel.Id, response.UserId);
        }

        [Test]
        public async Task CheckGetUsers_success()
        {
            var mockUsersModel = fixture.Build<UserModel>().CreateMany();
            userDataAccessMock.Setup(
                m => m.GetAllUsers()).Returns(Task.FromResult(mockUsersModel.ToList()));
            // Act
            var response = await userDataService.GetUsers();
            // Assert
            var returnUser = response.Users.Where(x => x.UserId == mockUsersModel.First().Id).First();
            Assert.IsNotNull(returnUser);
        }
    }
}