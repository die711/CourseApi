using System.Linq.Expressions;
using CourseApi.Dto.Specifications;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null,bool tracked= true, string? includeProperties = null);
    PagedList<T> GetAllPagination(Params parameters, Expression<Func<T, bool>>? filter = null, 
        string? includeProperties = null);
    Task<T?> Get(Expression<Func<T, bool>>? filter = null,bool tracked= true, string? includeProperties = null);
    Task<T?> GetById(int id);
    Task Create(T entity);
    Task Update();
    Task Save();
    Task Remove(T entity);

}