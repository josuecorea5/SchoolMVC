using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	public class StudentRepository : Repository<Student>, IStudentRepository
	{
		private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Student student)
		{
			_context.Students.Update(student);
		}
	}
}
