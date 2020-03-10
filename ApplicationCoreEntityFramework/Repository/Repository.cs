using ApplicationCoreEntityFramework.EntityBase;
using ApplicationCoreEntityFramework.Repository.Base;
using System.Data.Entity;

namespace ApplicationCoreEntityFramework.Repository
{
    public class Repository<T> : RepositoryBase<T> where T : class, IEntityBaseForDb, new()
    {

        public Repository(DbContext db) : base(db)
        {
        }
    }
}
