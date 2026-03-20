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
            return _context.Users.FirstOrDefault(x => x.UserName == name)!;
        }

        public int Update(User entity)
        {
            try
            {
                var tempEntity = _context.Users.Find(entity.UserId)!;
                if (tempEntity == null)
                    throw new Exception("Update failed. User is not found");
                tempEntity = entity;
                int row = _context.SaveChanges();
                return row;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public int Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    throw new Exception("Delete failed. User is not found");
                _context.Users.Remove(user);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public bool Create(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
