using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface IEnrollmentRepository : IRepository<Enrollment>
	{
		void Update(Enrollment enrollment);
	}
}
