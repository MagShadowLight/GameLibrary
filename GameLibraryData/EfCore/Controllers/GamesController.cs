using GameLibraryData.EfCore.Context;
using GameLibraryData.EfCore.Entities;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var tempEntity = _context.Games.Find(entity.GameId)!;
                if (tempEntity == null)
                    throw new Exception("Update failed. Game is not found");
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
                var games = _context.Games.Find(id);
                if (games == null)
                    throw new Exception("Delete failed. Game is not found");
                _context.Games.Remove(games);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public bool Create(Game entity)
        {
            try
            {
                _context.Games.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<Game> GetGamesByGenre(string genre)
        {
            return _context.Games
                .FromSqlRaw("EXEC dbo.SelectGamesByGenre @Genre", new SqlParameter("@Genre", genre))
                .ToList();
        }

        public int UpdateGameTitle(string genre, string title)
        {
            List<Game> games = GetGamesByGenre(genre);
            Game game = games.First();
            game.Title = title;
            return _context.SaveChanges();
        }
    }
}
