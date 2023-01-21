using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TelexistenceProject;
using static TelexistenceProject.UsersData;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var chanell = GrpcChannel.ForAddress("https://localhost:7020");
            var client = new UsersData.UsersDataClient(chanell);

            Menu();
            var command = Console.ReadLine();

            while (command != "exit") {
                try
                {
                    switch (command)
                    {
                        case "users":
                            await GetUsers(client);
                            break;
                        case "user":
                            Console.WriteLine("Enter user id: ");
                            var userId = Console.ReadLine();
                            await GetUserById(client, userId);
                            break;
                        case "delete":
                            Console.WriteLine("Enter delete user id: ");
                            userId = Console.ReadLine();
                            await DeleteUserById(client, userId);
                            break;
                        case "create":
                            var newUser = new UserModelRequest();
                            Console.WriteLine("Enter new user first name: ");
                            newUser.FirstName = Console.ReadLine();
                            Console.WriteLine("Enter new user second name: ");
                            newUser.SecondName = Console.ReadLine();
                            await CreateUser(client, newUser);
                            break;
                        case "update":
                            newUser = new UserModelRequest();
                            Console.WriteLine("Enter updated user id: ");
                            newUser.UserId = Console.ReadLine();
                            Console.WriteLine("Enter new first name: ");
                            newUser.FirstName = Console.ReadLine();
                            Console.WriteLine("Enter new second name: ");
                            newUser.SecondName = Console.ReadLine();
                            await UpdateUser(client, newUser);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex) { 
                    Console.WriteLine($"Something bad happends, try again.");
                }
                command = Console.ReadLine();
            }
            Console.ReadKey();
        }

        static void Menu()
        {
            Console.WriteLine($"Command list:\n" +
                $"users\n" +
                $"user\n" +
                $"delete\n" +
                $"create\n" +
                $"update\n");
        }

        static async Task GetUserById (UsersDataClient client, string userId)
        {
            var user = await client.GetUserAsync(
               new UserIdRequest
               {
                   UserId = userId
               });
            Console.WriteLine(user);
        }        
        
        static async Task CreateUser (UsersDataClient client, UserModelRequest newUser)
        {
            var user = await client.CreateUserAsync(newUser);
            Console.WriteLine(user);
        }

        static async Task UpdateUser(UsersDataClient client, UserModelRequest newUser)
        {
            var users = await client.UpdateUserAsync(newUser);
            foreach (var item in users.Users)
            {
                Console.WriteLine(item);
            }
        }

        static async Task DeleteUserById(UsersDataClient client, string userId)
        {
            var users = await client.DeleteUserAsync(
               new UserIdRequest
               {
                   UserId = userId
               });
            if (users.Users.Count == 0)
            {
                Console.WriteLine("There is no users");
            }
            foreach (var item in users.Users)
            {
                Console.WriteLine(item);
            }
        }

        static async Task GetUsers(UsersDataClient client)
        {
            var users = await client.GetUsersAsync(
               new Ping());
            if (users.Users.Count == 0)
            {
                Console.WriteLine("There is no users");
            }
            foreach (var item in users.Users)
            {
                Console.WriteLine(item);
            }
        }
    }
}
