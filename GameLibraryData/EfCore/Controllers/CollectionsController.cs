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
            throw new NotImplementedException();
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
