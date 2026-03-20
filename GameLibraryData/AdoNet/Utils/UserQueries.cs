using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class UserQueries
    {
        public string SelectAllUserQuery = """
                    SELECT UserId,
                           UserName,
                           DateofBirth,
                           Password,
                           Region,
                           Bios,
                           DateCreated,
                           Email
                    FROM dbo.Users;
                    """;
        public string SelectUserByIdQuery = """
                    SELECT UserId,
                           Username,
                           DateofBirth,
                           Password,
                           Region,
                           Bios,
                           DateCreated,
                           Email
                    FROM dbo.Users
                    WHERE UserId = @Id;
                    """;
        public string SelectUserByUserNameQuery = """
                    SELECT UserId,
                           Username,
                           DateofBirth,
                           Password,
                           Region,
                           Bios,
                           DateCreated,
                           Email
                    FROM dbo.Users
                    WHERE Username = @Username;
                    """;
        public string InsertUserQuery = """
                    INSERT INTO dbo.Users
                    VALUES (
                        @Username,
                        @DateofBirth,
                        @Password,
                        @Region,
                        @Bios,
                        @DateCreated,
                        @Email
                    )
                    """;
        public string UpdateUserQuery = """
                    Update dbo.Users
                    SET UserName = @Username,
                        Password = @Password,
                        Region = @Region,
                        Bios = @Bios
                    WHERE UserId = @Id;
                    """;
        public string DeleteUserQuery = """
                    DELETE FROM dbo.Users
                    WHERE UserId = @Id;
                    """;
    }
}
