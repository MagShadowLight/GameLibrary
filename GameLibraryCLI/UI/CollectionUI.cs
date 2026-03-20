using GameLibraryCLI.Exceptions;
using GameLibraryCLI.Utils;
using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Repository;
using GameLibraryData.EfCore.Controllers;
using GameLibraryData.EfCore.Entities;

namespace GameLibraryCLI.UI
{
    public class CollectionUI
    {
        private string _ConnectionString;
        private CollectionsController _collectionsController = new CollectionsController();
        private CollectionRepository _collectionsrepo;
        private GamesRepository _gamesrepo;
        private UserRepository _usersrepo;
        private GamesController _gamesController = new GamesController();
        private UsersController _userController = new UsersController();
        private Printer _printer = new Printer();
        public CollectionUI(string connectionstring)
        {
            _ConnectionString = connectionstring;
            _collectionsrepo = new CollectionRepository(_ConnectionString);
            _gamesrepo = new GamesRepository(_ConnectionString);
            _usersrepo = new UserRepository(_ConnectionString);
        } 
        public void RunCollectionsAccessUI(string dataAccessSystem)
        {
            while (true)
            {
                try {
                    Console.Clear();
                    Console.WriteLine("""
                        Select which operation to use?
                        1. Get all collections
                        2. Get collection by id
                        3. Get collection by User's name
                        4. Insert collection
                        5. Update collection
                        6. Delete collection
                        0. Exit
                        """);
                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (dataAccessSystem.ToLower().Equals("ado.net"))
                        {
                            switch (input)
                            {
                                case 1:
                                    var collections = GetAllCollections();
                                    _printer.PrintListOfCollectionWithRelatedData(collections);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    collections = GetAllCollections();
                                    var collection = GetCollectionById(collections)!;
                                    _printer.PrintCollectionWithRelatedData(collection);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    collection = GetCollectionByUserName()!;
                                    _printer.PrintCollectionWithRelatedData(collection);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultBool = InsertCollection();
                                    Console.WriteLine($"Rows added: {rowResultBool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    collections = GetAllCollections();
                                    var rowResult = UpdateCollection(collections);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    collections = GetAllCollections();
                                    rowResult = DeleteCollection(collections);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    Console.WriteLine("Invalid Choice. Please Try Again.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            switch (input)
                            {
                                case 1:
                                    List<Collection> collections = GetAllCollectionsEFCore();
                                    _printer.PrintListOfCollectionWithRelatedData(collections);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    collections = GetAllCollectionsEFCore();
                                    var collection = GetCollectionByIdEFCore(collections)!;
                                    _printer.PrintCollectionWithRelatedData(collection);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    collection = GetCollectionByUserNameEFCore();
                                    _printer.PrintCollectionWithRelatedData(collection);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultBool = InsertCollectionEFCore();
                                    Console.WriteLine($"Rows added: {rowResultBool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    collections = GetAllCollectionsEFCore();
                                    var rowResult = UpdateCollection(collections);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    collections = GetAllCollectionsEFCore();
                                    rowResult = DeleteCollection(collections);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    Console.WriteLine("Invalid Choice. Please Try Again.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    _printer.PrintError(ex.Message);
                    Console.Clear();
                }
            }                
        }

        private bool InsertCollectionEFCore()
        {
            Console.Clear();
            try
            {
                Collection collection = new Collection();
                List<User> users = _userController.GetAll();
                Console.Write("Enter the id of the user. it must be either ");
                foreach (User user in users)
                {
                    if (user != users.Last())
                        Console.Write($"{user.UserId}, ");
                    else
                        Console.WriteLine($"{user.UserId}");
                }
                bool result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("ERROR: failed to parse User Id");
                else if (!users.Where(u => u.UserId == userId).Any())
                    throw new Exception("ERROR: failed to find user by id");
                collection.UserId = userId;
                Console.Write("Enter the id of the game. It must be either ");
                List<Game> games = _gamesController.GetAll();
                foreach (Game game in games)
                {
                    if (game != games.Last())
                        Console.Write($"{game.GameId}, ");
                    else
                        Console.WriteLine($"{game.GameId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("ERROR: failed to parse Game Id");
                else if (!games.Where(u => u.GameId == gameId).Any())
                    throw new Exception("ERROR: failed to find game by id");
                collection.GameId = gameId;
                Console.WriteLine("Enter the Date that the user last played");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime date))
                    collection.DateLastPlayed = date;
                else
                    throw new Exception("Failed to parse date");
                Console.WriteLine("Enter the times the user have played");
                result = int.TryParse(Console.ReadLine()!, out int timesPlayed);
                if (!result)
                    throw new Exception("ERROR: failed to parse integer");
                collection.TimesPlayed = timesPlayed;
                result = _collectionsController.Create(collection);
                if (!result)
                    throw new Exception("Failed to create collection.");
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private Collection GetCollectionByUserNameEFCore()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the Name of the user");
            var name = Console.ReadLine()!;
            Console.WriteLine("Getting a collection by user name");
            return _collectionsController.GetByName(name);
        }

        private Collections? GetCollectionByUserName()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the Name of the user");
            var name = Console.ReadLine()!;
            Console.WriteLine("Getting a collection by user name");
            return _collectionsrepo.GetByName(name);
        }
        private int DeleteCollection(List<Collection> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {collections.Last().CollectionId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _collectionsController.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private  int DeleteCollection(List<Collections> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {collections.Last().CollectionId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _collectionsrepo.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int UpdateCollection(List<Collection> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the collection");
                var result = int.TryParse(Console.ReadLine()!, out int collectionId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the user Id");
                var collection = _collectionsController.GetById(collectionId);
                Console.Write("Enter the new id of the user. It must be either: ");
                List<User> users = _userController.GetAll();
                foreach (User user in users)
                {
                    if (user != users.Last())
                        Console.Write($"{user.UserId}, ");
                    else
                        Console.WriteLine($"{user.UserId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("ERROR: failed to parse User Id");
                else if (!users.Where(u => u.UserId == userId).Any())
                    throw new Exception("ERROR: failed to find user by id");
                collection.UserId = userId;
                Console.Write("Enter the new id of the game. It must be either ");
                List<Game> games = _gamesController.GetAll();
                foreach (Game game in games)
                {
                    if (game != games.Last())
                        Console.Write($"{game.GameId}, ");
                    else
                        Console.WriteLine($"{game.GameId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("ERROR: failed to parse Game Id");
                else if (!games.Where(u => u.GameId == gameId).Any())
                    throw new Exception("ERROR: failed to find game by id");
                collection.GameId = gameId;
                Console.WriteLine("Enter the Date that the user last played");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime date))
                    collection.DateLastPlayed = date;
                else
                    throw new Exception("Failed to parse date");
                Console.WriteLine("Enter the times the user have played");
                result = int.TryParse(Console.ReadLine()!, out int timesPlayed);
                if (!result)
                    throw new Exception("ERROR: failed to parse integer");
                collection.TimesPlayed = timesPlayed;
                var num = _collectionsController.Update(collection);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private  int UpdateCollection(List<Collections> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the collection");
                var result = int.TryParse(Console.ReadLine()!, out int collectionId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the user Id");
                var collection = _collectionsrepo.GetById(collectionId);
                Console.Write("Enter the new id of the user. It must be either: ");
                List<Users> users = _usersrepo.GetAll();
                foreach (Users user in users)
                {
                    if (user != users.Last())
                        Console.Write($"{user.UserId}, ");
                    else
                        Console.WriteLine($"{user.UserId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("ERROR: failed to parse User Id");
                else if (!users.Where(u => u.UserId == userId).Any())
                    throw new Exception("ERROR: failed to find user by id");
                collection.UserId = userId;
                Console.Write("Enter the new id of the game. It must be either ");
                List<Games> games = _gamesrepo.GetAll();
                foreach (Games game in games)
                {
                    if (game != games.Last())
                        Console.Write($"{game.GameId}, ");
                    else
                        Console.WriteLine($"{game.GameId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("ERROR: failed to parse Game Id");
                else if (!games.Where(u => u.GameId == gameId).Any())
                    throw new Exception("ERROR: failed to find game by id");
                collection.GameId = gameId;
                Console.WriteLine("Enter the Date that the user last played");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime date))
                    collection.DateLastPlayed = date;
                else
                    throw new Exception("Failed to parse date");
                Console.WriteLine("Enter the times the user have played");
                result = int.TryParse(Console.ReadLine()!, out int timesPlayed);
                if (!result)
                    throw new Exception("ERROR: failed to parse integer");
                collection.TimesPlayed = timesPlayed;
                var num = _collectionsrepo.Update(collection);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }

        private  bool InsertCollection()
        {
            Console.Clear();
            try
            {
                Collections collection = new Collections();
                List<Users> users = _usersrepo.GetAll();
                Console.Write("Enter the id of the user. it must be either ");
                foreach (Users user in users)
                {
                    if (user != users.Last())
                        Console.Write($"{user.UserId}, ");
                    else
                        Console.WriteLine($"{user.UserId}");
                }
                bool result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("ERROR: failed to parse User Id");
                else if (!users.Where(u => u.UserId == userId).Any())
                    throw new Exception("ERROR: failed to find user by id");
                collection.UserId = userId;
                Console.Write("Enter the id of the game. It must be either ");
                List<Games> games = _gamesrepo.GetAll();
                foreach (Games game in games)
                {
                    if (game != games.Last())
                        Console.Write($"{game.GameId}, ");
                    else
                        Console.WriteLine($"{game.GameId}");
                }
                result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("ERROR: failed to parse Game Id");
                else if (!games.Where(u => u.GameId == gameId).Any())
                    throw new Exception("ERROR: failed to find game by id");
                collection.GameId = gameId;
                Console.WriteLine("Enter the Date that the user last played");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime date))
                    collection.DateLastPlayed = date;
                else
                    throw new Exception("Failed to parse date");
                Console.WriteLine("Enter the times the user have played");
                result = int.TryParse(Console.ReadLine()!, out int timesPlayed);
                if (!result)
                    throw new Exception("ERROR: failed to parse integer");
                collection.TimesPlayed = timesPlayed;
                result = _collectionsrepo.Create(collection);
                if (!result)
                    throw new Exception("Failed to create collection.");
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private  Collections? GetCollectionById(List<Collections> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the number between 1 and {collections.Last().CollectionId}");
                if (int.TryParse(Console.ReadLine(), out int id))
                    Console.WriteLine("Getting a collection by Id");
                else
                    throw new Exception("ERROR: Failed to parse the collection id");
                return _collectionsrepo.GetById(id);
            } catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return null;
            }
        }       
        
        private  List<Collection> GetAllCollectionsEFCore()
        {
            Console.Clear();
            Console.WriteLine("Getting all collections");
            return _collectionsController.GetAll();
        }
        private  List<Collections> GetAllCollections()
        {
            Console.Clear();
            Console.WriteLine("Getting all collections");
            return _collectionsrepo.GetAll();
        }
        private  Collection? GetCollectionByIdEFCore(List<Collection> collections)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the number between 1 and {collections.Last().CollectionId}");
                bool result = int.TryParse(Console.ReadLine()!, out int id);
                if (!result)
                    throw new Exception("Failed to parse collection id");
                Console.WriteLine("Getting a collection by Id");
                return _collectionsController.GetById(id);
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);                
                return null;
            }
        }        
    }
}
