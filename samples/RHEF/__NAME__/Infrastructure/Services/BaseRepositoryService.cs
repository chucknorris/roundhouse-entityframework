using System.Linq;
using __NAME__.Domain;
using __NAME__.Infrastructure.Persistence;

namespace __NAME__.Infrastructure.Services
{
    /// <summary>
    /// Base class that implements most of the functionality necessary for a repository service class, minus custom queries.
    /// </summary>
    /// <typeparam name="T">A class that implements <see cref="IDomainObject"/> with a default constructor</typeparam>
    public abstract class BaseRepositoryService<T> : IRepositoryService<T>
        where T : class, IDomainObject, new()
    {
        public IRepository Repository { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepositoryService&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseRepositoryService(IRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="TKeyType">The type of the key.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<TKeyType>(TKeyType key)
        {
            return Repository.Get<T, TKeyType>(key);
        }
        
        public IQueryable<T> GetAll()
        {
            return Repository.GetAll<T>();
        }

        public object InsertOnCommit(T entity)
        {
            return Repository.InsertOnCommit(entity);
        }

        public void DeleteOnCommit(T entity)
        {
            Repository.DeleteOnCommit(entity);
        }

        public void CommitChanges()
        {
            Repository.CommitChanges();
        }
    }
}