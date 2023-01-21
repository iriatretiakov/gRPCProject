using DAL.DataAccess;
using DAL.Models;
using MongoDB.Driver;

namespace DAL.DataAccesss
{
    public class UserDataAccess : IUserDataAccess
    {
        private const string ConnectionString = "mongodb://127.0.0.1:27017";
        private const string DatabaseName = "usersdb";
        private const string UserCollection = "users";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var result = await usersCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task<UserModel> GetUser(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var result = await usersCollection.FindAsync(x => x.Id == user.Id);
            return result.First();
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            await usersCollection.InsertOneAsync(user);
            var result = await usersCollection.FindAsync(x => x.Id == user.Id);
            return result.First();
        }

        public Task DeleteUser(UserModel user) 
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            return usersCollection.DeleteOneAsync(x => x.Id == user.Id);
        }

        public Task UpdateUser(UserModel user)
        {
            var usersCollection = ConnectToMongo<UserModel>(UserCollection);
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            return usersCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = false });
        }
    }
}
