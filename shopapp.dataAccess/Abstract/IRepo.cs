using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.dataAccess.Abstract
{
    public interface IRepo<T>
    {

        T getById(int id);
        List<T> getAll();
        void create(T entity);
        void update(T entity);
        void delete(T entity);
    }
}