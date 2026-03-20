using GameLibraryCLI.Exceptions;
using GameLibraryCLI.Utils;
using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Repository;
using GameLibraryData.EfCore.Controllers;
using GameLibraryData.EfCore.Entities;

namespace GameLibraryCLI.UI
{
    public class GameUI
    {
        private string _ConnectionString;
        private Printer _printer = new Printer();
        private GamesRepository _gamesrepo;
        private GamesController _gamesController = new GamesController();
        public GameUI(string connectionstring)
        {
            _ConnectionString = connectionstring;
            _gamesrepo = new GamesRepository(_ConnectionString);
        }

        public void RunGamesAccessUI(string dataAccessSystem)
        {
            while (true)
            {
                try {
                    Console.Clear();
                    if (dataAccessSystem.ToLower().Equals("ado.net"))
                    {
                        Console.WriteLine("""
                            Select which operations to use?
                            1. Get all games
                            2. Get game by id
                            3. Get game by title
                            4. Insert game
                            5. Update game
                            6. Delete game
                            7. Update and Delete game using transaction
                            0. Exit
                            """);
                        if (int.TryParse(Console.ReadLine(), out int input))
                        {

                            switch (input)
                            {
                                case 1:
                                    var games = GetAllGames();
                                    _printer.PrintListOfGames(games);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    games = GetAllGames();
                                    var game = GetGameById(games)!;
                                    _printer.PrintGame(game);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    game = GetGameByTitle();
                                    _printer.PrintGame(game);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultbool = InsertGame();
                                    Console.WriteLine($"Rows added: {rowResultbool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    games = GetAllGames();
                                    var rowResult = UpdateGame(games);
                                    Console.WriteLine($"Row affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    games = GetAllGames();
                                    rowResult = DeleteGame(games);
                                    Console.WriteLine($"Row affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 7:
                                    games = GetAllGames();
                                    var strResult = UseTransaction(games);
                                    Console.WriteLine(strResult);
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    Console.WriteLine("Invalid choice. Please try again.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Failed to parse int");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("""
                            Select which operations to use?
                            1. Get all games
                            2. Get game by id
                            3. Get game by title
                            4. Insert game
                            5. Update game
                            6. Delete game
                            7. Get game by genre
                            8. Update game genre by title
                            0. Exit
                            """);
                        if (int.TryParse(Console.ReadLine(), out int input))
                        {
                            switch (input)
                            {
                                case 1:
                                    var games = GetAllGamesEFCore();
                                    _printer.PrintListOfGames(games);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    games = GetAllGamesEFCore();
                                    var game = GetGameByIdEFCore(games)!;
                                    _printer.PrintGame(game);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    game = GetGameByTitleEFCore();
                                    _printer.PrintGame(game);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultbool = InsertGameEFCore();
                                    Console.WriteLine($"Rows added: {rowResultbool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    games = GetAllGamesEFCore();
                                    var rowResult = UpdateGame(games);
                                    Console.WriteLine($"Row affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    games = GetAllGamesEFCore();
                                    rowResult = DeleteGame(games);
                                    Console.WriteLine($"Row affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 7:
                                    games = GetAllGamesEFCore();
                                    games = GetGameByGenre(games)!;
                                    _printer.PrintListOfGames(games);
                                    Console.ReadKey();
                                    break;
                                case 8:
                                    games = GetAllGamesEFCore();
                                    rowResult = UpdateGameTitleByGenre(games);
                                    if (rowResult >= -1)
                                        Console.WriteLine($"Row affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    Console.WriteLine("Invalid choice. Please try again.");
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

        private int UpdateGameTitleByGenre(List<Game> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.Write($"Enter the genre of the game.");
                List<string> tempvalue = new List<string>();
                foreach (var tempgame in games)
                {
                    if (!tempvalue.Contains(tempgame.Genre))
                        tempvalue.Add(tempgame.Genre);
                }
                Console.WriteLine($"It must be either {string.Join(", ", tempvalue)}");
                string genre = Console.ReadLine()!;
                if (!tempvalue.Contains(genre))
                    throw new Exception("ERROR: Invalid genre");
                Console.WriteLine("Enter the new title of the game");
                string title = Console.ReadLine()!;                
                Console.WriteLine("Updating the game");
                var num = _gamesController.UpdateGameTitle(genre, title);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }

        private List<Game>? GetGameByGenre(List<Game> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.Write($"Enter the genre of the game.");
                List<string> tempvalue = new List<string>();
                foreach (var game in games)
                {
                    if (!tempvalue.Contains(game.Genre))
                        tempvalue.Add(game.Genre);
                }
                Console.WriteLine($"It must be either {string.Join(", ", tempvalue)}");                
                string genre = Console.ReadLine()!;
                if (!tempvalue.Contains(genre))
                    throw new Exception("ERROR: unable to find games");
                Console.WriteLine("Getting games by Genre");
                return _gamesController.GetGamesByGenre(genre);
            } catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return null;
            }
        }

        private object InsertGameEFCore()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
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
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private Game GetGameByTitleEFCore()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Game game;
            Console.WriteLine($"Enter the title of the game.");
            string input = Console.ReadLine()!;
            game = _gamesController.GetByName(input);
            Console.Clear();
            return game;
        }

        private bool InsertGame()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Games game = new Games();
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
                game.UnitPrice = price;
                result = _gamesrepo.Create(game);
                if (!result)
                    throw new Exception("Failed to create game.");
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private List<Games> GetAllGames()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("Getting all games");
            return _gamesrepo.GetAll();
        }               

        private Game? GetGameByIdEFCore(List<Game> games)
        {
            try
            {
                ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
                Console.WriteLine($"Enter the number between 1 and {games.Last().GameId}");
                if (int.TryParse(Console.ReadLine(), out int id))
                    Console.WriteLine("Getting a user by Id");
                else
                    throw new InvalidIdException("");
                return _gamesController.GetById(id);
            }
            catch (InvalidIdException ex)
            {
                _printer.PrintError(ex.Message);
                return null;
            }
        }

        private List<Game> GetAllGamesEFCore()
        {
            Console.Clear();
            Console.WriteLine("Getting all games");
            return _gamesController.GetAll();            
        }

        private string UseTransaction(List<Games> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            int gameId = 0;
            bool result = false;
            Games game = GetGameById(games)!;
            Console.WriteLine("Enter the new title of the game");
            game.Title = Console.ReadLine()!;
            Console.WriteLine("Enter the new genre for the game");
            game.Genre = Console.ReadLine()!;
            Console.WriteLine($"Enter the number between 1 and {games.Last().GameId}");
            if (int.TryParse(Console.ReadLine()!, out gameId))
                result = _gamesrepo.UpdateAndDeleteGames(game, gameId);
            return result ? "Games have been updated and deleted successfully with transaction." : "Update and delete games failed miserably. Transaction have been rolled back.";
        }
        private int DeleteGame(List<Game> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {games.Last().GameId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _gamesController.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int DeleteGame(List<Games> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {games.Last().GameId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _gamesrepo.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int UpdateGame(List<Game> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the user from 1 and {games.Last().GameId}");
                var result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the game Id");
                var game = _gamesController.GetById(gameId);
                Console.WriteLine("Enter the new title of the game");
                game.Title = Console.ReadLine()!;
                Console.WriteLine("Enter the new Developer");
                game.Developer = Console.ReadLine()!;
                Console.WriteLine("Enter the new Publisher");
                game.Publisher = Console.ReadLine()!;
                Console.WriteLine("Enter the new Release Date");
                result = DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate);
                if (!result)
                    throw new Exception("ERROR: Failed to parse release date");
                game.ReleaseDate = releaseDate;
                Console.WriteLine("Enter the new Genre");
                game.Genre = Console.ReadLine()!;
                Console.WriteLine("Enter the new Price in two decimal");
                result = decimal.TryParse(Console.ReadLine()!, out decimal price);
                if (!result)
                    throw new Exception("ERROR: Failed to parse price");
                game.Prices = price;
                Console.WriteLine("Updating the game");
                var num = _gamesController.Update(game);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }

        private int UpdateGame(List<Games> games)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the user from 1 and {games.Last().GameId}");
                var result = int.TryParse(Console.ReadLine()!, out int gameId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the game Id");
                var game = _gamesrepo.GetById(gameId);
                Console.WriteLine("Enter the new title of the game");
                game.Title = Console.ReadLine()!;
                Console.WriteLine("Enter the new Developer");
                game.Developer = Console.ReadLine()!;
                Console.WriteLine("Enter the new Publisher");
                game.Publisher = Console.ReadLine()!;
                Console.WriteLine("Enter the new Release Date");
                result = DateTime.TryParse(Console.ReadLine()!, out DateTime releaseDate);
                if (!result)
                    throw new Exception("ERROR: Failed to parse release date");
                game.ReleaseDate = releaseDate;
                Console.WriteLine("Enter the new Genre");
                game.Genre = Console.ReadLine()!;
                Console.WriteLine("Enter the new Price in two decimal");
                result = decimal.TryParse(Console.ReadLine()!, out decimal price);
                if (!result)
                    throw new Exception("ERROR: Failed to parse price");
                game.UnitPrice = price;
                Console.WriteLine("Updating the game");
                var num = _gamesrepo.Update(game);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private Games GetGameByTitle()
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Games game;
            Console.WriteLine($"Enter the title of the game.");
            string input = Console.ReadLine()!;
            game = _gamesrepo.GetByName(input);
            Console.Clear();
            return game;
        }

        private Games? GetGameById(List<Games> games)
        {
            try
            {
                ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
                Console.WriteLine($"Enter the number between 1 and {games.Last().GameId}");
                if (int.TryParse(Console.ReadLine(), out int id))
                    Console.WriteLine("Getting a user by Id");
                else
                    throw new InvalidIdException("");
                return _gamesrepo.GetById(id);
            }
            catch (InvalidIdException ex)
            {
                _printer.PrintError(ex.Message);
                return null;
            }
        }        
    }
}
