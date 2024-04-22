using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface IStudyPlanRepository : IRepository<StudyPlan>
	{
		void Update(StudyPlan entity);
	}
}
