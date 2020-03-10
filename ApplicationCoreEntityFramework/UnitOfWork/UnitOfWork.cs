using ApplicationCoreEntityFramework.UnitOfWork.Base;
using System.Data.Entity;

namespace ApplicationCoreEntityFramework.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(DbContext db) : base(db)
        {
        }
    }
}