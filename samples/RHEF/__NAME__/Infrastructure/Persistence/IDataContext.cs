using System.Data.Entity;

namespace __NAME__.Infrastructure.Persistence
{
    /// <summary>
    /// The data context used work with the database. This interface repeats items already in the DbContext
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Grabs a set, can be used with linq queries to limit results
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}