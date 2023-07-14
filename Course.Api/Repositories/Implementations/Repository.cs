using System.Linq.Expressions;
using CourseApi.DataAccess;
using CourseApi.Dto.Specifications;
using CourseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _db;
    protected readonly DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, bool tracked = true,
        string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return await query.ToListAsync();
    }

    public PagedList<T> GetAllPagination(Params parameters, Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return PagedList<T>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<T?> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true,
        string? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);
        
        if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task Create(T entity)
    {
        await dbSet.AddAsync(entity);
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
        dbSet.Remove(entity);
        await Save();
    }
}