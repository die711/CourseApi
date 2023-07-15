using CourseApi.DataAccess;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<bool> isUniqueUser(string UserName)
    {
        var user =await dbSet.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower());
        return user == null;
    }

    public async Task Registrar(User user)
    {
        await dbSet.AddAsync(user);
        await Save();
    }
}