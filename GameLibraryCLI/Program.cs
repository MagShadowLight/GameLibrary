using GameLibraryCLI.Utils;
using GameLibraryData.Models;
using GameLibraryData.Repository;

namespace GameLibraryCLI
{
    internal class Program
    {
        
        private static string _ConnectionString;
        private static GamesRepository _repo;
        static void Main(string[] args)
        {
            _ConnectionString = "Server=localhost;Database=GameLibrary;Trusted_Connection=True;TrustServerCertificate=True;";
            _repo = new GamesRepository(_ConnectionString);
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("Getting all games");
            List<Games> games = _repo.GetAll();
            PrintListOfGames(games);
            Thread.Sleep(3000);
            Console.WriteLine("Press any key to get game from id");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Select any number between 1 and {games.Count}");
            string input = Console.ReadLine()!;
            Games game = new Games();
            if (int.TryParse(input, out int id))
            {
                game = _repo.GetById(id);
            } else
            {
                Console.WriteLine($$"""
                    ERROR:
                    Invalid Id Number. Please type in a number instead of {{input}}
                    """);
            }
            Console.Clear();
            PrintGame(game);
            Console.WriteLine("Press any key to get game from title");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Enter the title of the game.");
            input = Console.ReadLine()!;
            game = _repo.GetByTitle(input);
            Console.Clear();
            PrintGame(game);
            Console.WriteLine("Press any key to Exit");
            Console.ReadKey();
            ConsoleColors.ChangeColor(ConsoleColor.Black, ConsoleColor.Gray);
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
            }
        }
    }
}
