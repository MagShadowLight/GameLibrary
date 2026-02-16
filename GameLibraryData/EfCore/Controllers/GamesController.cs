using GameLibraryData.EfCore.Context;
using GameLibraryData.EfCore.Entities;
using GameLibraryData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.EfCore.Controllers
{
    public class GamesController : IDataAccess<Game>
    {
        private GameLibraryDbContext _context = new GameLibraryDbContext();

        public List<Game> GetAll()
        {
            return _context.Games.ToList();
        }

        public Game GetById(int id)
        {
            return _context.Games.FirstOrDefault(x => x.GameId == id)!;
        }

        public Game GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(Game entity)
        {
            throw new NotImplementedException();
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
