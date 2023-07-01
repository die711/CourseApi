using System.Linq.Expressions;

namespace CourseApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{

    Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    
}