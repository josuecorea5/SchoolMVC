using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface ISubjectRepository : IRepository<Subject>
	{
		void Update(Subject subject);
	}
}
