namespace MoviesMVC.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(int? id);
        Task<IEnumerable<T>?> GetAll(string[]? includes = null);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> criteria);
        Task<T?> Find(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<IEnumerable<T>?> FindAll(Expression<Func<T, bool>> criteria,
             string[]? includes = null, int? skip = null, int? take = null,
            Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending);
    }
}