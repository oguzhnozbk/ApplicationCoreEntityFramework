using ApplicationCoreEntityFramework.EntityBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ApplicationCoreEntityFramework.Repository.Base
{
    public abstract class RepositoryBase<T> : IDisposable where T : class, IEntityBaseForDb, new()
    {
        protected DbContext _db;

        protected RepositoryBase(DbContext db)
        {
            _db = db;
        }

        #region Dispose Pattern

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
        /// <summary>
        /// Verilen Entity için liste döndürür.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            try
            {
                return _db.Set<T>().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity için Predicate ile belirtilen koşulu karşılayan öğeleri içeren liste döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _db.Set<T>().Where(predicate).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity için Query döndürür.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetQueryable()
        {
            try
            {
                return _db.Set<T>().AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity için Predicate ile belirtilen koşulu karşılayan öğeleri içeren Query döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _db.Set<T>().Where(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Belirtilen Primary Key değeri için Entity döndürür. Primary Key'e bağlı Entity bulamazsa ve id değeri geçersizse null değer döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int? id)
        {
            try
            {
                return id.HasValue ? _db.Set<T>().Find(id) : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity için Predicate ile belirtilen koşulu karşılayan öğeyi döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _db.Set<T>().SingleOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity yi Veritabanına ekleneceğini ifade eder. Ekleme yapılabilmesi için SaveChanges yapılması gereklidir.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            try
            {
                _db.Set<T>().Add(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verilen Entity yi Veritabanında güncelleneceğini ifade eder. Güncellemenin yapılabilmesi için SaveChanges yapılması gereklidir.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            try
            {
                _db.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verilen Entity yi Veritabanından silineceğini ifade eder. Silinebilmesi için SaveChanges yapılması gereklidir.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity.GetType().GetProperty("IsDeleted") != null)
                {
                    entity.GetType().GetProperty("IsDeleted")?.SetValue(entity, true);
                    Update(entity);
                }
                else
                {
                    _db.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verilen Primary Key değerine sahip Entity Veritabanından silineceğini ifade eder. Silinebilmesi için SaveChanges yapılması gereklidir.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(int? id)
        {
            try
            {
                var entity = Get(id);
                Delete(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
