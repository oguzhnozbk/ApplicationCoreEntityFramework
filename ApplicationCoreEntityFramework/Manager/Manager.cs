using ApplicationCoreEntityFramework.EntityBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ApplicationCoreEntityFramework.Manager
{
    public class Manager<T> : IDisposable where T : class, IEntityBaseForDb, new()
    {
        private Repository.Repository<T> _repository;
        private UnitOfWork.UnitOfWork _unitOfWork;

        public Manager(DbContext db)
        {
            try
            {
                if (db != null)
                {
                    _repository = new Repository.Repository<T>(db);
                    _unitOfWork = new UnitOfWork.UnitOfWork(db);
                }
                else throw new Exception($"DbContext can't be null !");

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #region Dispose Pattern

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository?.Dispose();
                _unitOfWork?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion

        /// <summary>
        /// Bu Context'te yapılan tüm değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            try
            {
                return _unitOfWork.Save();
            }
            catch (Exception exc)
            {
                throw exc;
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
                return _repository.GetQueryable();
            }
            catch (Exception exc)
            {
                throw exc;
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
                return _repository.GetQueryable(predicate);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Verilen Entity için Liste döndürür.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            try
            {
                return _repository.GetList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Verilen Entity için Predicate ile belirtilen koşulu karşılayan öğreleri içeren List döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _repository.GetList(predicate);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        ///  Belirtilen Primary Key değeri için Entity döndürür. Primary Key'e bağlı Entity bulamazsa ve id değeri geçersizse null değer döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Find(int? id)
        {
            try
            {
                return _repository.Get(id);
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
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _repository.Get(predicate);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Gönderilen Entityi veritabanına ekler. Eğer save parametresi false olarak gönderilirse bu işlemden sonra verinin veritabanına eklenebilmesi için Save() yapılması gerekir.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        public virtual void Add(T entity, bool save = true)
        {
            try
            {
                _repository.Add(entity);
                if (save)
                    _unitOfWork.Save();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Gönderilen Entityi veritabanında günceller. Eğer save parametresi false olarak gönderilirse bu işlemden sonra verinin veritabanında güncellenebilmesi için Save() yapılması gerekir.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        public virtual void Update(T entity, bool save = true)
        {
            try
            {
                _repository.Update(entity);
                if (save)
                    _unitOfWork.Save();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Gönderilen Entityi veritabanından siler. Eğer save parametresi false olarak gönderilirse bu işlemden sonra verinin veritabanından silinebilmesi için Save() yapılması gerekir.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        public virtual void Delete(T entity, bool save = true)
        {
            try
            {
                _repository.Delete(entity);
                if (save)
                    _unitOfWork.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Gönderilen Primary Key değerine sahip Entityi veritabanından siler. Eğer save parametresi false olarak gönderilirse bu işlemden sonra verinin veritabanından silinebilmesi için Save() yapılması gerekir.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="save"></param>
        public virtual void Delete(int id, bool save = true)
        {
            try
            {
                _repository.Delete(id);
                if (save)
                    _unitOfWork.Save();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Predicate koşuluna uyan bir veri olup olmadığını kontrol eder. eğer veri varsa true döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _repository.GetQueryable().Any(predicate);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Dizideki öğelerin sayısını döndürür.
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            try
            {
                return _repository.GetQueryable().Count();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        /// <summary>
        /// Dizideki verilen Predicate koşulunu sağlayan öğelerin sayısını döndürür.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _repository.GetQueryable(predicate).Count();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

    }
}
