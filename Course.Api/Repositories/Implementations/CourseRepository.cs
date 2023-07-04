using CourseApi.DataAccess;
using CourseApi.Entities;
using CourseApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Repositories.Implementations;

public class CourseRepository :Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<List<Course>> TopRanking(int number)
    {
        return await dbSet.OrderByDescending(v => v.Ranking).Take(number).ToListAsync();
    }
}