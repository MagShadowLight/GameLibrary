using EncryptorTools;
using GameLibraryCLI.UI;
using GameLibraryCLI.Utils;

namespace GameLibraryCLI
{
    internal class Program
    {
        // Connection String: "Server=localhost;Database=GameLibrary;Trusted_Connection=True;TrustServerCertificate=True;"
        private static string _ConnectionString = Encrypter.Decrypt("connectionstring.txt");
        private static CollectionUI collectionUI = new CollectionUI(_ConnectionString);
        private static UserUI userUI = new UserUI(_ConnectionString);
        private static GameUI gameUI = new GameUI(_ConnectionString);
        private static Printer _printer = new Printer();
        static void Main(string[] args)
        {
            Console.Title = "Game Library CLI";
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine("Starting Game Library App");
            Thread.Sleep(1500);
            Console.Clear();
            RunUI();
            ConsoleColors.ChangeColor(ConsoleColor.Black, ConsoleColor.Gray);
        }

        private static void RunUI()
        {
            while (true)
            {
                Console.WriteLine("Which Data Access System do you want to use? ADO.NET or EFCore.\nType exit to Quit the application");
                var input = Console.ReadLine()!;
                switch (input.ToLower())
                {
                    case "ado.net":
                        AccessTableUI(input);
                        break;
                    case "efcore":
                        AccessTableUI(input);
                        break;
                    case "exit":
                        Console.WriteLine("Goodbye");
                        return;
                    default:
                        _printer.PrintError("Invalid choice. Please Try Again.");
                        break;
                }
                Console.Clear();
            }
        }

        private static void AccessTableUI(string dataAccessSystem)
        {
            Console.Clear();
            Console.WriteLine("Which table to access to? games, users, or collections");
            var input = Console.ReadLine()!;
            switch (input.ToLower())
            {
                case "games":
                    gameUI.RunGamesAccessUI(dataAccessSystem);
                    break;
                case "users":
                    userUI.RunUsersAccessUI(dataAccessSystem);
                    break;
                case "collections":
                    collectionUI.RunCollectionsAccessUI(dataAccessSystem);
                    break;
                default:
                    _printer.PrintError("Invalid Choice. Please Try Again.");
                    break;
            }
        }
    }
}
