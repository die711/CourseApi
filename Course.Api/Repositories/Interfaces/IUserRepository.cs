using System.Diagnostics.CodeAnalysis;
using CourseApi.Entities;

namespace CourseApi.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<Boolean>isUniqueUser(string UserName);
    Task Registrar(User user);
}