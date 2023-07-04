using CourseApi.Entities;

namespace CourseApi.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
        Task<List<Course>> TopRanking(int number);
}