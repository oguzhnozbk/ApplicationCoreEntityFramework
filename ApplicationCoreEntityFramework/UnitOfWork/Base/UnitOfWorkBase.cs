using System;
using System.Data.Entity;

namespace ApplicationCoreEntityFramework.UnitOfWork.Base
{
    public abstract class UnitOfWorkBase : IDisposable
    {
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

        protected DbContext _db;

        protected UnitOfWorkBase(DbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Verilen Entity için SaveChanges işlemini gerçekleştirir.
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}