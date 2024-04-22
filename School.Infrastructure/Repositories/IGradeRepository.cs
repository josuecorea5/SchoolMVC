using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface IGradeRepository : IRepository<Grade>
	{
		void Update(Grade grade);
	}
}
