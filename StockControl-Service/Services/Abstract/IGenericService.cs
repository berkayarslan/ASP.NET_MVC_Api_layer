using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_Service.Services.Abstract
{
    public interface IGenericService<T>
    {
        bool Add(T entity);
        bool Add(List<T> items);
        bool Update(T entity);
        bool Remove(T entity);
        bool Remove(int id);
        bool RemoveAll(Expression<Func<T, bool>> expression);
        T GetById(int id);
        T GetByDefault(Expression<Func<T, bool>> expression);
        IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes);//ilişkilerini de dahil ederek getireceğiz
        List<T> GetActive();//db de aktif olanları getirir
        IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes);//aktif olanları ilişkileri ile getireceğiz
        List<T> GetDefault(Expression<Func<T, bool>> expression);//şarta uyanların hepsini getirelim..
        List<T> GetAll();//db de T tipli herkes gelsin
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);// belirli bir filtrelemeyi geçen herkes tüm ilişkileri ile gelsin 
        bool GetActive(int id);//aktif edelim. bizde db den silme yok pasife çekmek var çünkü
        bool Any(Expression<Func<T, bool>> expression);
        int Save();
    }
}
