using Microsoft.EntityFrameworkCore;
using StockControl_Entity.Entities.Abstract;
using StockControl_Repository.Context;
using StockControl_Repository.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace StockControl_Repository.Repos.Concretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                return Save() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Add(List<T> items)
        {
            //transactionscope -- rollback: geri almak, complete: tamamlamak işlem ok. 
            //burda şunu yapıcaz birden fazla ekleme yapacaz işlem ortada kalırsa bi yerde hata alırsa geriye alacak işlemleri 
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //bu transaction newlendiği yerde eğer complete denmezse yaptığımız entity değişiklikleri kayıt olmaz 
                    //yani save desek bile cınolete gelmediyse kaydetmez...
                    foreach (T item in items)
                    {
                        _context.Set<T>().Add(item);
                    }
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public List<T> GetActive()
        {
            return _context.Set<T>().Where(x => x.IsActive).ToList();
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => x.IsActive);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

        public bool GetActive(int id) //nesneyi aktif hale getirir. 
        {
            T entity = GetById(id);
            entity.IsActive = true;
            return Update(entity);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            //herhangi bir filtreleme olmadan dbset tablsoundaki herkesi ilişkilerini dahil ederek getirelim.
            var query = _context.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {

            var query = _context.Set<T>().Where(expression);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;


        }

        public T GetByDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().FirstOrDefault(expression);

        public T GetById(int id) => _context.Set<T>().Find(id);

        public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            //burda query orneğin bir tablomuz 
            //burda eager kullandık 
            //çünkü tablo eager virtual demedik lazy kullanmadık
            //eager dediğimiz için biz kendimiz ilişkisini belirtmek zorundayız 
            //o yuzden burda joinler var
            //buranın açıklaması derste var .10.09.2024 dersi tahmini 1.20 saat dakika
            var query = _context.Set<T>().Where(a => a.ID == id);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).ToList();

        public bool Remove(T entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }

        public bool Remove(int id)
        {
            try
            {
               T item= GetById(id);
                return Remove(item);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            using(TransactionScope ts= new TransactionScope())
            {
                var collection = GetDefault(expression);
                int counter= 0;
                foreach (var item in collection)
                {
                    item.IsActive = false;
                    bool operation=Update(item);
                    if(operation) counter++;
                }
                if (collection.Count == counter) ts.Complete();
                else return false;
            }
            return true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T entity)
        {
            try
            {
                entity.ModifiedDate = DateTime.Now;
                _context.Set<T>().Update(entity);
                return Save() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
