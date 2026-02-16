using GameLibraryData.EfCore.Context;
using GameLibraryData.EfCore.Entities;
using GameLibraryData.Interface;

namespace GameLibraryData.EfCore.Controllers
{
    public class UsersController : IDataAccess<User>
    {
        private GameLibraryDbContext _context = new GameLibraryDbContext();

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == id)!;
        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(User entity)
        {
            throw new NotImplementedException();
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
