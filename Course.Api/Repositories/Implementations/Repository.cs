using System.Linq.Expressions;
using CourseApi.DataAccess;
using CourseApi.Dto.Specifications;
using CourseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> _dbSet;
    
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, bool tracked = true,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        return await query.ToListAsync();
    }

    public PagedList<T> GetAllPagination(Params parameters, Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null)
    {

        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);
        
        return PagedList<T>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<T?> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Create(T entity)
    {
        await _dbSet.AddAsync(entity);
        await Save();
    }

    public async Task Update()
    {
        await Save();
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        _dbSet.Remove(entity);
        await Save();
    }
}