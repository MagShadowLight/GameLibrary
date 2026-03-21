using GameLibraryCLI.Exceptions;
using GameLibraryCLI.Utils;
using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Repository;
using GameLibraryData.EfCore.Controllers;
using GameLibraryData.EfCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryCLI.UI
{
    public class UserUI
    {
        private string _ConnectionString;
        private UserRepository _usersrepo;
        private UsersController _userController = new UsersController();
        private Printer _printer = new Printer();
        public UserUI(string connectionstring)
        {
            _ConnectionString = connectionstring;
            _usersrepo = new UserRepository(_ConnectionString);
        }
        public void RunUsersAccessUI(string dataAccessSystem)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("""
                        Select which operation to use?
                        1. Get all users
                        2. Get user by id
                        3. Get user by username
                        4. Insert user
                        5. Update user
                        6. Delete user
                        0. Exit
                        """);
                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (dataAccessSystem.ToLower().Equals("ado.net"))
                        {
                            switch (input)
                            {
                                case 1:
                                    var users = GetAllUsers();
                                    Console.Clear();
                                    _printer.PrintListOfUsers(users);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    users = GetAllUsers();
                                    var user = GetUserById(users)!;
                                    _printer.PrintUsers(user);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    user = GetUserByUserName();
                                    _printer.PrintUsers(user);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultBool = InsertUser();
                                    Console.WriteLine($"Rows added: {rowResultBool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    users = GetAllUsers();
                                    var rowResult = UpdateUser(users);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    users = GetAllUsers();
                                    rowResult = DeleteUser(users);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    _printer.PrintError("Invalid choice. Please try again.");
                                    break;
                            }
                        }
                        else
                        {
                            switch (input)
                            {
                                case 1:
                                    var users = GetAllUsersEFCore();
                                    _printer.PrintListOfUsers(users);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    users = GetAllUsersEFCore();
                                    var user = GetUserByIdEFCore(users)!;
                                    _printer.PrintUsers(user);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    user = GetUserByUserNameEFCore();
                                    _printer.PrintUsers(user);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    var rowResultBool = InsertUserEFCore();
                                    Console.WriteLine($"Rows added: {rowResultBool}");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    users = GetAllUsersEFCore();
                                    var rowResult = UpdateUser(users);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 6:
                                    users = GetAllUsersEFCore();
                                    rowResult = DeleteUser(users);
                                    if (rowResult > -1)
                                        Console.WriteLine($"Rows affected: {rowResult}");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    return;
                                default:
                                    _printer.PrintError("Invalid choice. Please try again.");
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _printer.PrintError(ex.Message);
                    Console.Clear();
                }
            }
        }
        private User? GetUserByIdEFCore(List<User> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the number between 1 and {users.Last().UserId}");
                bool result = int.TryParse(Console.ReadLine()!, out int id);
                if (!result)
                    throw new Exception("ERROR: Failed to parse user Id");
                Console.WriteLine("Getting a user by Id");
                return _userController.GetById(id);
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return null;
            }
        }

        private List<User> GetAllUsersEFCore()
        {
            Console.Clear();
            Console.WriteLine("Getting all users");
            return _userController.GetAll();
        }

        private int DeleteUser(List<User> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {users.Last().UserId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _userController.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int DeleteUser(List<Users> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            Console.WriteLine($"Enter the number between 1 and {users.Last().UserId}");
            try
            {
                if (int.TryParse(Console.ReadLine()!, out int id))
                    return _usersrepo.Delete(id);
                else
                    throw new InvalidIdException("");
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int UpdateUser(List<User> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the user from 1 and {users.Last().UserId}");
                var result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the user Id");
                var user = _userController.GetById(userId);
                Console.WriteLine("Enter the new Username");
                user.UserName = Console.ReadLine()!;
                Console.WriteLine("Enter the new password");
                user.Password = Console.ReadLine()!;
                Console.WriteLine("Enter the new region");
                user.Region = Console.ReadLine()!;
                Console.WriteLine("Enter the new bios");
                user.Bios = Console.ReadLine()!;
                Console.WriteLine("Updating the user");
                var num = _userController.Update(user);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private int UpdateUser(List<Users> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the id of the user from 1 and {users.Last().UserId}");
                var result = int.TryParse(Console.ReadLine()!, out int userId);
                if (!result)
                    throw new Exception("Update failed. Failed to parse the user Id");
                var user = _usersrepo.GetById(userId);
                Console.WriteLine("Enter the new Username");
                user.UserName = Console.ReadLine()!;
                Console.WriteLine("Enter the new password");
                user.Password = Console.ReadLine()!;
                Console.WriteLine("Enter the new region");
                user.Region = Console.ReadLine()!;
                Console.WriteLine("Enter the new bios");
                user.Bios = Console.ReadLine()!;
                Console.WriteLine("Updating the user");
                var num = _usersrepo.Update(user);
                return num;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return -1;
            }
        }
        private bool InsertUserEFCore()
        {
            Console.Clear();
            try
            {
                User user = new User();
                Console.WriteLine("Enter the name of the user");
                user.UserName = Console.ReadLine()!;
                Console.WriteLine("Enter the Date of Birth of the user");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime dateOfBirth))
                    user.DateofBirth = dateOfBirth;
                else
                    throw new Exception("Failed to parse date of birth");
                Console.WriteLine("Enter the password of the user");
                user.Password = Console.ReadLine()!;
                Console.WriteLine("Enter the region of the user in two letter");
                user.Region = Console.ReadLine()!;
                Console.WriteLine("Enter the bios of the user");
                user.Bios = Console.ReadLine()!;
                Console.WriteLine("Enter the email of the user");
                user.Email = Console.ReadLine()!;
                user.DateCreated = DateTime.Now;
                bool result = _userController.Create(user);
                if (!result)
                    throw new Exception("Failed to create user.");
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private bool InsertUser()
        {
            Console.Clear();
            try
            {
                Users user = new Users();
                Console.WriteLine("Enter the name of the user");
                user.UserName = Console.ReadLine()!;
                Console.WriteLine("Enter the Date of Birth of the user");
                if (DateTime.TryParse(Console.ReadLine()!, out DateTime dateOfBirth))
                    user.DateofBirth = dateOfBirth;
                else
                    throw new Exception("Failed to parse date of birth");
                Console.WriteLine("Enter the password of the user");
                user.Password = Console.ReadLine()!;
                Console.WriteLine("Enter the region of the user in two letter");
                user.Region = Console.ReadLine()!;
                Console.WriteLine("Enter the bios of the user");
                user.Bios = Console.ReadLine()!;
                Console.WriteLine("Enter the email of the user");
                user.Email = Console.ReadLine()!;
                user.DateCreated = DateTime.Now;
                bool result = _usersrepo.Create(user);
                if (!result)
                    throw new Exception("Failed to create user.");
                return result;
            }
            catch (Exception ex)
            {
                _printer.PrintError(ex.Message);
                return false;
            }
        }

        private User GetUserByUserNameEFCore()
        {
            Console.Clear();
            Console.WriteLine($"Enter the name of the user");
            string name = Console.ReadLine()!;
            Console.WriteLine("Getting a user by username");
            var user = _userController.GetByName(name);
            Console.Clear();
            return user;
        }
        private Users GetUserByUserName()
        {
            Console.Clear();
            Console.WriteLine($"Enter the name of the user");
            string name = Console.ReadLine()!;
            Console.WriteLine("Getting a user by username");
            var user = _usersrepo.GetByName(name);
            Console.Clear();
            return user;
        }
        private Users? GetUserById(List<Users> users)
        {
            ConsoleColors.ChangeColor(ConsoleColor.Blue, ConsoleColor.White);
            try
            {
                Console.WriteLine($"Enter the number between 1 and {users.Last().UserId}");
                if (int.TryParse(Console.ReadLine(), out int id))
                    Console.WriteLine("Getting a user by Id");
                else
                    throw new Exception("ERROR: Failed to parse user Id");
                    return _usersrepo.GetById(id);
            } catch (Exception e)
            {
                _printer.PrintError(e.Message);
                return null;
            }
        }
        private List<Users> GetAllUsers()
        {
            Console.Clear();
            Console.WriteLine("Getting all users");
            return _usersrepo.GetAll();
        }        
    }
}
