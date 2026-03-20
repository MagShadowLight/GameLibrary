using GameLibraryData.AdoNet.Models;
using GameLibraryData.EfCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryCLI.Utils
{
    public class Printer
    {
        // Games
        
        public void PrintGame(Game game)
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
        public void PrintGame(Games game)
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
        public void PrintListOfGames(List<Games> games, string indent = "")
        {
            foreach (Games game in games)
            {
                Console.WriteLine($$"""
                    {{indent}}Id={{game.GameId}}
                    {{indent}}Title={{game.Title}}
                    {{indent}}Developer={{game.Developer}}
                    {{indent}}Publisher={{game.Publisher}}
                    {{indent}}Release Date={{game.ReleaseDate}}
                    {{indent}}Genre={{game.Genre}}
                    {{indent}}Price={{game.UnitPrice}}
                    """);
                Thread.Sleep(1500);
            }
        }
        public void PrintListOfGames(List<Game> games)
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

        // Users

        public void PrintListOfUsers(List<Users> users, string indent = "")
        {
            foreach (Users user in users)
            {
                Console.WriteLine($"""
                    {indent}UserId={user.UserId}
                    {indent}UserName={user.UserName}
                    {indent}Date of Birth={user.DateofBirth.ToString()}
                    {indent}Password={user.Password}
                    {indent}Region={user.Region}
                    {indent}Bios={user.Bios}
                    {indent}Date Created={user.DateCreated.ToString()}
                    {indent}Email={user.Email}
                    """);
                Thread.Sleep(1500);
            }
        }
        public void PrintListOfUsers(List<User> users)
        {
            Console.Clear();
            foreach (User user in users)
            {
                Console.WriteLine($"""
                    UserId={user.UserId}
                    UserName={user.UserName}
                    Date of Birth={user.DateofBirth.ToString()}
                    Password={user.Password}
                    Region={user.Region}
                    Bios={user.Bios}
                    Date Created={user.DateCreated.ToString()}
                    Email={user.Email}
                    """);
                Thread.Sleep(1500);
            }
        }
        public void PrintUsers(Users user)
        {
            Console.WriteLine($$"""
                    UserId={{user.UserId}}
                    UserName={{user.UserName}}
                    DateOfBirth={{user.DateofBirth}}
                    Password={{user.Password}}
                    Region={{user.Region}}
                    Bios={{user.Bios}}
                    DateCreated={{user.DateCreated}}
                    Email={{user.Email}}
                    """);
            Thread.Sleep(1500);
        }
        public void PrintUsers(User user)
        {
            Console.WriteLine($$"""
                    UserId={{user.UserId}}
                    UserName={{user.UserName}}
                    DateOfBirth={{user.DateofBirth}}
                    Password={{user.Password}}
                    Region={{user.Region}}
                    Bios={{user.Bios}}
                    DateCreated={{user.DateCreated}}
                    Email={{user.Email}}
                    """);
            Thread.Sleep(1500);
        }

        // Collections

        public void PrintListOfCollectionWithRelatedData(List<Collections> collections)
        {
            foreach (var collection in collections)
            {
                Console.WriteLine($$"""
                Collection Id={{collection.CollectionId}}
                User Id={{collection.UserId}}
                Game Id={{collection.GameId}}
                Date Last Played={{collection.DateLastPlayed}}
                Times Played={{collection.TimesPlayed}}
                """);
                Console.WriteLine("Users: ");
                PrintListOfUsers(collection.users, "    ");
                Console.WriteLine("Games: ");
                PrintListOfGames(collection.games, "    ");                
            }
        }
        public void PrintListOfCollectionWithRelatedData(List<Collection> collections)
        {
            foreach (var collection in collections)
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
                Thread.Sleep(1500);
            }
        }
        public void PrintCollectionWithRelatedData(Collections collection)
        {
            Console.WriteLine($$"""
                Collection Id={{collection.CollectionId}}
                User Id={{collection.UserId}}
                Game Id={{collection.GameId}}
                Date Last Played={{collection.DateLastPlayed}}
                Times Played={{collection.TimesPlayed}}
                """);
            Console.WriteLine("Users: ");
            PrintListOfUsers(collection.users, "    ");
            Console.WriteLine("Games: ");
            PrintListOfGames(collection.games, "    ");
        }
        public void PrintCollectionWithRelatedData(Collection collection)
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

        public void PrintError(string message)
        {
            ConsoleColors.ChangeColor(ConsoleColor.DarkRed, ConsoleColor.White);
            Console.WriteLine($$"""
                    ERROR:
                    {{message}}
                    """);
            Console.ReadKey();
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
        }
    }
}
