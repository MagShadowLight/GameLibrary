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
    public class CollectionsController : IDataAccess<Collection>
    {
        private GameLibraryDbContext _context = new GameLibraryDbContext();
        public List<Collection> GetAll()
        {
            return _context.Collections.ToList();
        }

        public Collection GetById(int id)
        {
            return _context.Collections.FirstOrDefault(x => x.CollectionId == id)!;
        }

        public Collection GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(Collection entity)
        {
            try
            {
                var tempEntity = _context.Collections.Find(entity.CollectionId)!;
                if (tempEntity == null)
                    throw new Exception("Update failed. Collection is not found");
                tempEntity = entity;
                int row = _context.SaveChanges();
                return row;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public int Delete(int id)
        {
            try
            {
                var collection = _context.Collections.Find(id);
                if (collection == null)
                    throw new Exception("Delete failed. Collection is not found");
                _context.Collections.Remove(collection);
                return _context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public bool Create(Collection entity)
        {
            try
            {
                _context.Collections.Add(entity);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
