using StockControl_Entity.Entities.Abstract;
using StockControl_Repository.Repos.Abstract;
using StockControl_Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_Service.Services.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : BaseEntity
    {
        private IGenericRepository<T> _repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public bool Add(T entity)
        {
            if (entity is not null)
                return _repository.Add(entity);
            else return false;
        }

        public bool Add(List<T> items)
        {
            if (items is not null)
                return _repository.Add(items);
            else return false;
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _repository.Any(expression);
        }

        public List<T> GetActive()
        {
            return _repository.GetActive();
        }

        public IQueryable<T> GetActive(params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetActive(includes);
        }

        public bool GetActive(int id)
        {
            if (id == 0 || GetById(id) == null) return false;
            else return _repository.GetActive(id);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetAll(includes);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetAll(expression, includes);
        }

        public T GetByDefault(Expression<Func<T, bool>> expression)
        {
            return _repository.GetByDefault(expression);
        }

        public T GetById(int id)
        {
            if (id == 0) return null;
            else return _repository.GetById(id);
        }

        public IQueryable<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            if (id != 0) return _repository.GetById(id, includes);
            else return null;
        }

        public List<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return _repository.GetDefault(expression);
        }

        public bool Remove(T entity)
        {
            if (entity == null) return false;
            else return _repository.Remove(entity);
        }

        public bool Remove(int id)
        {
            if (id == 0) return false;
            else return _repository.Remove(id);
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            return _repository.RemoveAll(expression);
        }

        public int Save()
        {
            return _repository.Save();
        }

        public bool Update(T entity)
        {
            if (entity == null) return false;
            else return _repository.Update(entity);
        }
    }
}
