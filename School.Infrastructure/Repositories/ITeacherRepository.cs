using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        void Update(Teacher student);
    }
}
