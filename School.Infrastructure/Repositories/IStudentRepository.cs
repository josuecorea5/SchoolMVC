using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface IStudentRepository : IRepository<Student>
	{
		void Update(Student student);
	}
}
