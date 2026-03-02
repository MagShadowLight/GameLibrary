using GameLibraryCLI.Exceptions;
using GameLibraryCLI.Utils;
using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Repository;
using GameLibraryData.EfCore.Controllers;
using GameLibraryData.EfCore.Entities;
using System.Diagnostics;

namespace GameLibraryCLI
{
    internal class Program
    {

        private static string _ConnectionString = Encryptor.Decrypt("connectionstring.txt");
        private static GamesRepository _repo = new GamesRepository(_ConnectionString);
        private static GamesController _gamesController = new GamesController();
        private static UsersController _userController = new UsersController();
        private static CollectionsController _collectionsController = new CollectionsController();
        static int GamesCount = _gamesController.GetAll().Count;
        static int UsersCount = _userController.GetAll().Count;
        static int CollectionCount = _collectionsController.GetAll().Count;
        static void Main(string[] args)
        {
            Console.Title = "Game Library CLI";
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("Starting Game Library App");
            // Week 8:
            Console.WriteLine("Getting the collection with both the user and game");
            Collection collection = ReadCollection();
            if (collection != null)
                PrintCollectionWithRelatedData(collection);

            // Week 7:
            
            //Console.WriteLine("Inserting the game into Game Library");
            //InsertGameEFCore();
            //Thread.Sleep(1000);
            //Console.WriteLine("Press any key to update User");
            //Console.ReadKey();
            //Console.Clear();
            //UpdateUserEFCore();
            //Console.WriteLine("Press any key to delete game from the game library");
            //Console.ReadKey();
            //Console.Clear();
            //DeleteGameEFCore();
            



            // Week 6:
            // Games:


            Console.WriteLine("Press any key to get all games");
            Console.ReadKey();
            Console.Clear();
            GetAllGamesEFCore();
            Console.WriteLine("Press any key to get game by Id");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Enter the number between 1 and {GamesCount}");
            if (int.TryParse(Console.ReadLine(), out int gameId))
                GetGameByIdEFCore(gameId);
            else
                Console.WriteLine($"Failed to parse the number");

            // Users:

            Console.WriteLine("Press any key to get all users");
            Console.ReadKey();
            Console.Clear();
            GetAllUsersEFCore();
            Console.WriteLine("Press any key to get user by Id");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Enter the number between 1 and {UsersCount}");
            if (int.TryParse(Console.ReadLine(), out int userId))
                GetUserByIdEFCore(userId);
            else
                Console.WriteLine($"Failed to parse the number");

            // Collections:

            Console.WriteLine("Press any key to get all collections");
            Console.ReadKey();
            Console.Clear();
            GetAllCollectionsEFCore();
            Console.WriteLine("Press any key to get collection by Id");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Enter the number between 1 and {CollectionCount}");
            if (int.TryParse(Console.ReadLine(), out int collectionId))
                GetCollectionByIdEFCore(collectionId);
            else
                Console.WriteLine($"Failed to parse the number");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();


            // Week 5:
            //CollectionRepository repo = new CollectionRepository(_ConnectionString);
            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //List<Collection> collections;
            //collections = repo.GetAll();
            //timer.Stop();
            //Console.WriteLine($$"""
            //    Get all collection of games user have owned:
            //    Time={{timer.ElapsedMilliseconds}} ms
            //    Command Count={{repo.CommandCount}}
            //    """);
            //Console.ReadKey();
            //Console.WriteLine();
            //Console.WriteLine("Collections: ");
            //foreach (var collection in collections)
            //{
            //    Console.WriteLine($$"""
            //        Id={{collection.CollectionId}}
            //        GameId={{collection.GameId}}
            //        UserId={{collection.UserId}}
            //        DateLastPlayed={{collection.DateLastPlayed}}
            //        TimesPlayed={{collection.TimesPlayed}}
            //        """);
            //    Console.WriteLine("Games:");
            //    foreach (var game in collection.games)
            //    {
            //        Console.WriteLine($$"""
            //            GameId={{game.GameId}}
            //            Title={{game.Title}}
            //            Developer={{game.Developer}}
            //            Publisher={{game.Publisher}}
            //            ReleaseDate={{game.ReleaseDate}}
            //            Genre={{game.Genre}}
            //            Price={{game.UnitPrice}}
            //            """);
            //    }
            //    Console.WriteLine("Users:");
            //    foreach(var user in collection.users)
            //    {
            //        Console.WriteLine($$"""
            //            UserId={{user.UserId}}
            //            UserName={{user.UserName}}
            //            DateofBirth={{user.DateofBirth}}
            //            Password={{user.Password}}
            //            Region={{user.Region}}
            //            Bios={{user.Bios}}
            //            DateCreated={{user.DateCreated}}
            //            Email={{user.Email}}
            //            """);
            //    }
            //    Console.ReadLine();
            //}

            // Previous Week
            //List<Games> games = GetAllGames();
            //PrintListOfGames(games);
            //PrintMessage("Press any key to get game from id");
            //Games game = GetGameById(games);
            //PrintGame(game);
            //PrintMessage("Press any key to get game from title");
            //game = GetGameByTitle();
            //PrintGame(game);
            //PrintMessage("Press any key to update game");
            //int rowAffected = UpdateGame(game, games);
            //if (rowAffected >= 0)
            //    Console.WriteLine($"Number of row updated: {rowAffected}");
            //PrintMessage("Press any key to delete game");
            //rowAffected = DeleteGame(games);
            //if (rowAffected >= 0)
            //    Console.WriteLine($"Number of row deleted: {rowAffected}");
            //PrintMessage("Press any key to use transaction");
            //string result = UseTransaction(games);
            //Console.WriteLine(result);
            //PrintMessage("Press any key to Exit");
            ConsoleColors.ChangeColor(ConsoleColor.Black, ConsoleColor.Gray);
        }

        private static void PrintCollectionWithRelatedData(Collection collection)
        {
            Console.WriteLine($$"""
                Collection Id={{collection.CollectionId}}
                User Id={{collection.UserId}}
                User:
                    UserName={{collection.User.UserName}}
                    Date of Birth={{collection.User.DateofBirth.ToString()}}
                    Password={{collection.User.Password}}
                    Region={{collection.User.Region}}
                    Bios={{collection.User.Bios}}
                    Date Created={{collection.User.DateCreated.ToString()}}
                    Email={{collection.User.Email}}
                Game Id={{collection.GameId}}
                Game:
                    Title={{collection.Game.Title}}
                    Developer={{collection.Game.Developer}}
                    Publisher={{collection.Game.Publisher}}
                    Release Date={{collection.Game.ReleaseDate.ToString()}}
                    Genre={{collection.Game.Genre}}
                    Price={{collection.Game.Prices.ToString()}}
                Date Last Played={{collection.DateLastPlayed}}
                Times Played={{collection.TimesPlayed}}
                """);
        }

        private static Collection ReadCollection()
        {
            int collectionCount = _collectionsController.GetAll().Count;
            Console.WriteLine($"Enter the number between 1 and {collectionCount}");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine($"Getting the collection by id: {id}");
            return _collectionsController.GetCollectionWithRelationshipById(id);
        }

        private static void DeleteGameEFCore()
        {
            try
            {
                var games = _gamesController.GetAll();
                Console.WriteLine($"Enter the number between 1 and {games.Count}");
                var result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("Delete failed. Failed to parse the game Id");
                Console.WriteLine("Deleting the game");
                int num = _gamesController.Delete(gameId);
                if (num == 0)
                    throw new Exception("Game deletion failed.");
                Console.WriteLine("Game deleted successfully");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private static void UpdateUserEFCore()
        {
            try
            {
                var users = _userController.GetAll();
                Console.WriteLine($"Enter the id of the user from 1 and {users.Count}");
                var result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the user Id");
                var user = _userController.GetById(userId);
                Console.WriteLine("Enter the Username");
                user.UserName = Console.ReadLine()!;
                Console.WriteLine("Enter the password");
                user.Password = Console.ReadLine()!;
                Console.WriteLine("Enter the region");
                user.Region = Console.ReadLine()!;
                Console.WriteLine("Enter the bios");
                user.Bios = Console.ReadLine()!;
                Console.WriteLine("Updating the user");
                var num = _userController.Update(user);
                if (num == 0)
                {
                    Console.WriteLine("failed to update user");
                    return;
                }
                Console.WriteLine("User updated successfully");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void InsertGameEFCore()
        {
            try
            {
                Game game = new Game();
                Console.WriteLine("Enter the title of the game");
                game.Title = Console.ReadLine()!;
                Console.WriteLine("Enter the developer of the game");
                game.Developer = Console.ReadLine()!;
                Console.WriteLine("Enter the publisher of the game");
                game.Publisher = Console.ReadLine()!;
                Console.WriteLine("Enter the release date of the game in yyyy-mm-dd");
                bool result = DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate);
                if (!result)
                    throw new Exception("Failed to parse release date");
                game.ReleaseDate = releaseDate;
                Console.WriteLine("Enter the genre of the game");
                game.Genre = Console.ReadLine()!;
                Console.WriteLine("Enter the price of the game with two decimal point");
                result = decimal.TryParse(Console.ReadLine()!, out decimal price);
                if (!result)
                    throw new Exception("Failed to parse price");
                game.Prices = price;
                result = _gamesController.Create(game);
                if (!result)
                    throw new Exception("Failed to create game.");
                Console.WriteLine("Create game successfully");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private static void GetCollectionByIdEFCore(int id)
        {
            Console.WriteLine("Getting a collection by Id");
            Collection collection = _collectionsController.GetById(id);
            Console.WriteLine("Collection retrieved. Press any key to display it.");
            Console.ReadKey();
            Console.Clear();
            string result = PrintCollection(collection);
            Console.WriteLine(result);
        }

        private static void GetAllCollectionsEFCore()
        {
            Console.WriteLine("Getting all collections");
            List<Collection> collections = _collectionsController.GetAll();
            Console.WriteLine($$"""
                all collections retrieved.
                Count of collections: {{collections.Count}}
                Press any key to display all collections.
                """);
            Console.ReadKey();
            Console.Clear();
            foreach (Collection collection in collections)
            {
                string result = PrintCollection(collection);
                Console.WriteLine(result);
                Thread.Sleep(1500);
            }
        }

        private static string PrintCollection(Collection collection)
        {
            return $$"""
                    CollectionId={{collection.CollectionId}}
                    UserId={{collection.UserId}}
                    GameId={{collection.GameId}}
                    DateLastPlayed={{collection.DateLastPlayed}}
                    TimesPlayed={{collection.TimesPlayed}}
                    """;
        }

        private static void GetUserByIdEFCore(int id)
        {
            Console.WriteLine("Getting a user by Id");
            User user = _userController.GetById(id);
            Console.WriteLine("User retrieved. Press any key to display it.");
            Console.ReadKey();
            Console.Clear();
            string result = PrintUsers(user);
            Console.WriteLine(result);
        }

        private static void GetAllUsersEFCore()
        {
            Console.WriteLine("Getting all users");
            List<User> users = _userController.GetAll();
            Console.WriteLine($$"""
                all users retrieved.
                Count of users: {{users.Count}}
                Press any key to display all users.
                """);
            Console.ReadKey();
            Console.Clear();
            foreach (User user in users)
            {
                string result = PrintUsers(user);
                Console.WriteLine(result);
                Thread.Sleep(1500);
            }
        }

        private static string PrintUsers(User user)
        {
            return $$"""
                    UserId={{user.UserId}}
                    UserName={{user.UserName}}
                    DateOfBirth={{user.DateofBirth}}
                    Password={{user.Password}}
                    Region={{user.Region}}
                    Bios={{user.Bios}}
                    DateCreated={{user.DateCreated}}
                    Email={{user.Email}}
                    """;
        }

        private static void GetGameByIdEFCore(int id)
        {
            Console.WriteLine("Getting a game by Id");
            Game game = _gamesController.GetById(id);
            Console.WriteLine("Game retrieved. Press any key to display it.");
            Console.ReadKey();
            Console.Clear();
            PrintGame(game);
        }

        private static void GetAllGamesEFCore()
        {
            Console.WriteLine("Getting all games");
            List<Game> games = _gamesController.GetAll();
            Console.WriteLine($$"""
                all games retrieved.
                Count of games: {{games.Count}}
                Press any key to display all games.
                """);
            Console.ReadKey();
            Console.Clear();
            PrintListOfGames(games);
        }

        private static string UseTransaction(List<Games> games)
        {
            int gameId = 0;
            bool result = false;
            Games game = GetGameById(games);
            Console.WriteLine("Enter the new title of the game");
            game.Title = Console.ReadLine()!;
            Console.WriteLine("Enter the new genre for the game");
            game.Genre = Console.ReadLine()!;
            Console.WriteLine($"Enter the number between 1 and {games.Count}");
            if (int.TryParse(Console.ReadLine()!, out gameId))
                result = _repo.UpdateAndDeleteGames(game, gameId);
            return result ? "Games have been updated and deleted successfully with transaction." : "Update and delete games failed miserably. Transaction have been rolled back.";
        }

        private static int DeleteGame(List<Games> games)
        {
            Console.WriteLine($"Enter the number between 1 and {games.Count}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _repo.Delete(id);
                else
                    throw new InvalidIdException("");
            } catch (Exception ex)
            {
                PrintError(ex.Message);
                return -1;
            }
        }

        private static int UpdateGame(Games game, List<Games> games)
        {
            game = GetGameById(games);
            Console.WriteLine($$"""
                Before Update:
                GameId={{game.GameId}}
                Title={{game.Title}}
                Developer={{game.Developer}}
                Publisher={{game.Publisher}}
                Release Date={{game.ReleaseDate}}
                Genre={{game.Genre}}
                UnitPrice={{game.UnitPrice}}
                """);
            Console.WriteLine("Enter the new title of the game");
            game.Title = Console.ReadLine()!;
            Console.WriteLine("Enter the new developer");
            game.Developer = Console.ReadLine()!;
            Console.WriteLine("Enter the new publisher");
            game.Publisher = Console.ReadLine()!;
            Console.WriteLine("Enter the new release date");
            try
            {
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate))
                    game.ReleaseDate = releaseDate;
                else
                    throw new InvalidReleaseDateException("");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
                return -1;
            }
            Console.WriteLine("Enter the new genre");
            game.Genre = Console.ReadLine()!;
            Console.WriteLine("Enter the new price in two decimal point");
            try
            {
                if (decimal.TryParse(Console.ReadLine()!, out decimal price))
                    game.UnitPrice = price;
                else
                    throw new InvalidPriceException("");
            } catch (Exception ex)
            {
                PrintError(ex.Message);
                return -1;
            }
            Console.WriteLine($$"""
                After Update:
                GameId={{game.GameId}}
                Title={{game.Title}}
                Developer={{game.Developer}}
                Publisher={{game.Publisher}}
                Release Date={{game.ReleaseDate}}
                Genre={{game.Genre}}
                UnitPrice={{game.UnitPrice}}
                """);
            return _repo.Update(game);
        }

        private static void PrintError(string message)
        {
            ConsoleColors.ChangeColor(ConsoleColor.DarkRed, ConsoleColor.White);
            Console.WriteLine($$"""
                    ERROR:
                    {{message}}
                    """);
            Console.ReadKey();
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
        }

        private static void PrintMessage(string prompt)
        {
            Thread.Sleep(3000);
            Console.WriteLine(prompt);
            Console.ReadKey();
            Console.Clear();
        }

        private static Games GetGameByTitle()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Games game;
            Console.WriteLine($"Enter the title of the game.");
            string input = Console.ReadLine()!;
            game = _repo.GetByName(input);
            Console.Clear();
            return game;
        }

        private static Games GetGameById(List<Games> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Games game = new Games();
            Console.WriteLine($"Select any number between 1 and {games.Count}");
            string input = Console.ReadLine()!;
            try
            {
                if (int.TryParse(input, out int id))
                    game = _repo.GetById(id);
                else
                    throw new InvalidIdException("");
            } catch (Exception ex)
            {
                PrintError(ex.Message);
            }
            Console.Clear();
            return game;
        }

        private static List<Games> GetAllGames()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("Getting all games");
            return _repo.GetAll();
        }

        private static void PrintGame(Games game)
        {
            Console.WriteLine($$"""
                    Id={{game.GameId}}
                    Title={{game.Title}}
                    Developer={{game.Developer}}
                    Publisher={{game.Publisher}}
                    Release Date={{game.ReleaseDate}}
                    Genre={{game.Genre}}
                    Price={{game.UnitPrice}}
                    """);
        }
        private static void PrintGame(Game game)
        {
            Console.WriteLine($$"""
                    Id={{game.GameId}}
                    Title={{game.Title}}
                    Developer={{game.Developer}}
                    Publisher={{game.Publisher}}
                    Release Date={{game.ReleaseDate}}
                    Genre={{game.Genre}}
                    Price={{game.Prices}}
                    """);
        }

        private static void PrintListOfGames(List<Games> games)
        {
            foreach (Games game in games)
            {
                Console.WriteLine($$"""
                    Id={{game.GameId}}
                    Title={{game.Title}}
                    Developer={{game.Developer}}
                    Publisher={{game.Publisher}}
                    Release Date={{game.ReleaseDate}}
                    Genre={{game.Genre}}
                    Price={{game.UnitPrice}}
                    """);
                Thread.Sleep(1500);
            }
        }
        private static void PrintListOfGames(List<Game> games)
        {
            foreach (Game game in games)
            {
                Console.WriteLine($$"""
                    Id={{game.GameId}}
                    Title={{game.Title}}
                    Developer={{game.Developer}}
                    Publisher={{game.Publisher}}
                    Release Date={{game.ReleaseDate}}
                    Genre={{game.Genre}}
                    Price={{game.Prices}}
                    """);
                Thread.Sleep(1500);
            }
        }
    }
}
