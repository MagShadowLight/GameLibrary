using GameLibraryCLI.Exceptions;
using GameLibraryCLI.Utils;
using GameLibraryData.Models;
using GameLibraryData.Repository;
using System.Diagnostics;

namespace GameLibraryCLI
{
    internal class Program
    {
        
        private static string _ConnectionString = "Server=localhost;Database=GameLibrary;Trusted_Connection=True;TrustServerCertificate=True;";
        private static GamesRepository _repo = new GamesRepository(_ConnectionString);
        static void Main(string[] args)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            CollectionRepository repo = new CollectionRepository(_ConnectionString);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            List<Collection> collections;
            collections = repo.GetAll();
            timer.Stop();
            Console.WriteLine($$"""
                Get all collection of games user have owned:
                Time={{timer.ElapsedMilliseconds}} ms
                Command Count={{repo.CommandCount}}
                """);
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Collections: ");
            foreach (var collection in collections)
            {
                Console.WriteLine($$"""
                    Id={{collection.CollectionId}}
                    GameId={{collection.GameId}}
                    UserId={{collection.UserId}}
                    DateLastPlayed={{collection.DateLastPlayed}}
                    TimesPlayed={{collection.TimesPlayed}}
                    """);
                Console.WriteLine("Games:");
                foreach (var game in collection.games)
                {
                    Console.WriteLine($$"""
                        GameId={{game.GameId}}
                        Title={{game.Title}}
                        Developer={{game.Developer}}
                        Publisher={{game.Publisher}}
                        ReleaseDate={{game.ReleaseDate}}
                        Genre={{game.Genre}}
                        Price={{game.UnitPrice}}
                        """);
                }
                Console.WriteLine("Users:");
                foreach(var user in collection.users)
                {
                    Console.WriteLine($$"""
                        UserId={{user.UserId}}
                        UserName={{user.UserName}}
                        DateofBirth={{user.DateofBirth}}
                        Password={{user.Password}}
                        Region={{user.Region}}
                        Bios={{user.Bios}}
                        DateCreated={{user.DateCreated}}
                        Email={{user.Email}}
                        """);
                }
                Console.ReadLine();
            }

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
    }
}
