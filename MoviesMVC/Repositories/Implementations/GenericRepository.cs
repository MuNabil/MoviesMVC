namespace MoviesMVC.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = TableWithInclude(includes);


            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> criteria,
            string[]? includes = null, int? skip = null, int? take = null,
            Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = TableWithInclude(includes);

            query = query.Where(criteria);

            if (skip.HasValue)
                query = query.Take(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);


            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(string[]? includes = null)
        {
            IQueryable<T> query = TableWithInclude(includes);

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<PagedList<T>> GetPageListAsync(PaginationParams pageInfo, Expression<Func<T, bool>> criteria, string[]? includes = null)
        {
            IQueryable<T> query = TableWithInclude(includes).AsNoTracking();
            query = query.Where(criteria);
            return await PagedList<T>.CreateAsync(query, pageInfo.PageNumber, pageInfo.PageSize);
        }

        private IQueryable<T> TableWithInclude(string[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query;
        }
    }
}