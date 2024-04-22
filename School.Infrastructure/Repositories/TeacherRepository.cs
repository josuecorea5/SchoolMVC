using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }
        public void Update(Teacher teacher)
        {
           _context.Teachers.Update(teacher);
        }
    }
}
