using System;
using System.Linq;
using __NAME__.Domain;

namespace __NAME__.Infrastructure.Persistence
{
    /// <summary>
    ///   The entity framework repository
    /// </summary>
    public class EntityFrameworkRepository : IRepository
    {
        private readonly IDataContext _dataContext;

        /// <summary>
        /// Gets the data context.
        /// </summary>
        protected IDataContext DataContext
        {
            get
            {
                return _dataContext;
            }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityFrameworkRepository" /> class.
        /// </summary>
        /// <param name="dataContext"> The data context. </param>
        public EntityFrameworkRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        ///   Commits the changes.
        /// </summary>
        public virtual void CommitChanges()
        {
            this.Log().Debug(() => "EF is saving all changes to the repository");
            _dataContext.SaveChanges();
        }

        /// <summary>
        ///   Gets an instance of <see cref="T" /> with the specified key.
        /// </summary>
        /// <typeparam name="T"> A class that implements <see cref="IDomainObject" /> with a default constructor </typeparam>
        /// <typeparam name="TKeyType">The type of the key</typeparam>
        /// <param name="key"> The key. </param>
        /// <returns> </returns>
        public virtual T Get<T, TKeyType>(TKeyType key) where T : class, IDomainObject, new()
        {
            this.Log().Debug(() => "EF is searching for '{0} ({1})'".FormatWith(typeof(T).Name, key));
            return _dataContext.Set<T>().Find(key);
        }

        /// <summary>
        ///   Gets all instances of <see cref="T" /> . Can be used with a linq query to limit results
        /// </summary>
        /// <typeparam name="T"> A class that implements <see cref="IDomainObject" /> with a default constructor </typeparam>
        /// <returns> A list of <see cref="T" /> </returns>
        public virtual IQueryable<T> GetAll<T>() where T : class, IDomainObject, new()
        {
            this.Log().Debug(() => "EF is performing a query for '{0}'".FormatWith(typeof(T).Name));
            return _dataContext.Set<T>();
        }

        /// <summary>
        ///   Inserts the specified entity instance of <see cref="T" /> on commit.
        /// </summary>
        /// <typeparam name="T"> A class that implements <see cref="IDomainObject" /> with a default constructor </typeparam>
        /// <param name="entity"> The entity. </param>
        /// <returns> The Id of the inserted item</returns>
        public virtual object InsertOnCommit<T>(T entity) where T : class, IDomainObject, new()
        {
            entity.SetInitialInsertProperties();
            this.Log().Debug(() => "EF is inserting a '{0}'".FormatWith(typeof(T).Name));
            _dataContext.Set<T>().Add(entity);

            object newKey = entity.GetKey();

            this.Log().Debug(() => "EF is has created '{0} ({1})'".FormatWith(typeof(T).Name, newKey));

            return newKey;
        }

        /// <summary>
        ///   Deletes the specified entity instance of <see cref="T" /> on commit.
        /// </summary>
        /// <typeparam name="T"> A class that implements <see cref="IDomainObject" /> with a default constructor </typeparam>
        /// <param name="entity"> The entity. </param>
        public virtual void DeleteOnCommit<T>(T entity) where T : class, IDomainObject, new()
        {
            this.Log().Debug(() => "EF is deleting '{0} ({1})'".FormatWith(typeof(T).Name, entity.GetKey()));
            _dataContext.Set<T>().Remove(entity);
        }
    }
}