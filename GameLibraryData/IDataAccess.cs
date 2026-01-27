using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData
{
    public interface IDataAccess<T>
    {
        public List<T> GetAll();

        public T GetById(int id);

        public T GetByName(string name);
        public int Update(T entity);
        public int Delete(int id);
    }
}
