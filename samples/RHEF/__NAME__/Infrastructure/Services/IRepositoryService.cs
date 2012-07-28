using System.Linq;
using __NAME__.Domain;

namespace __NAME__.Infrastructure.Services
{
    /// <summary>
    ///  Base items that sit on top of IRepository but allow us to only have one instance of IRepository during execution. 
    /// </summary>
    /// <typeparam name="T">A class that implements <see cref="IDomainObject"/> with a default constructor</typeparam>
    public interface IRepositoryService<T> 
        where T : class, IDomainObject, new()
    {
        /// <summary>
        /// Gets an instance of <see cref="T"/> with the specified key.
        /// </summary>
        /// <typeparam name="TKeyType">The type of the key</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>An instance of <see cref="T"/> if it finds one with the key; otherwise null</returns>
        T Get<TKeyType>(TKeyType key);

        /// <summary>
        /// Gets all instances of <see cref="T"/>. Can be used with a linq query to limit results
        /// </summary>
        /// <returns>A list of <see cref="T"/></returns>
        IQueryable<T> GetAll();
        
        /// <summary>
        /// Inserts the specified entity instance of <see cref="T"/> on commit.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        object InsertOnCommit(T entity);
        
        /// <summary>
        /// Deletes the specified entity instance of <see cref="T"/> on commit.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteOnCommit(T entity);
        
        /// <summary>
        /// Commits the changes.
        /// </summary>
        void CommitChanges();
    }
}