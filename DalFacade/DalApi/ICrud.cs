using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
     public  interface ICrud<T>
    {
        public  void Add(T t);
        public  void Update(int Id1,int Id2);
        public T Get(int Id1,int Id2);
        public void Delete(int Id1, int Id2);
        public IEnumerable<T> GetList();
    }
}
