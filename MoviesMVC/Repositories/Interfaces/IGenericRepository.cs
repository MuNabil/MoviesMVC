namespace MoviesMVC.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int? id);
        Task<IEnumerable<T>?> GetAllAsync(string[]? includes = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task<PagedList<T>> GetPageListAsync(PaginationParams pagInfo, Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes = null);
        Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> criteria,
             string[]? includes = null, int? skip = null, int? take = null,
            Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending);
    }
}