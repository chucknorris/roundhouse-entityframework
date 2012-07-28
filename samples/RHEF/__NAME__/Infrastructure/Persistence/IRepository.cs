using System;
using System.Linq;
using __NAME__.Domain;

namespace __NAME__.Infrastructure.Persistence
{
    /// <summary>
    /// The repository for working with the database.
    /// </summary>
    public interface IRepository //: IDisposable
    {
        /// <summary>
        /// Commits the changes.
        /// </summary>
        void CommitChanges();

        /// <summary>
        /// Gets an instance of <see cref="T"/> with the specified key.
        /// </summary>
        /// <typeparam name="T">A class that implements <see cref="IDomainObject"/> with a default constructor</typeparam>
        /// <typeparam name="TKeyType">The type of the key</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T, TKeyType>(TKeyType key) where T : class, IDomainObject, new();

        /// <summary>
        /// Gets all instances of <see cref="T"/>. Can be used with a linq query to limit results
        /// </summary>
        /// <typeparam name="T">A class that implements <see cref="IDomainObject"/> with a default constructor</typeparam>
        /// <returns>A list of <see cref="T"/></returns>
        IQueryable<T> GetAll<T>() where T : class, IDomainObject, new();
        
        /// <summary>
        ///   Inserts the specified entity instance of <see cref="T" /> on commit.
        /// </summary>
        /// <typeparam name="T"> A class that implements <see cref="IDomainObject" /> with a default constructor </typeparam>
        /// <param name="entity"> The entity. </param>
        /// <returns> The Id of the inserted item</returns>
        object InsertOnCommit<T>(T entity) where T : class, IDomainObject, new();

        /// <summary>
        /// Deletes the specified entity instance of <see cref="T"/> on commit.
        /// </summary>
        /// <typeparam name="T">A class that implements <see cref="IDomainObject"/> with a default constructor</typeparam>
        /// <param name="entity">The entity.</param>
        void DeleteOnCommit<T>(T entity) where T : class, IDomainObject, new();
    }
}