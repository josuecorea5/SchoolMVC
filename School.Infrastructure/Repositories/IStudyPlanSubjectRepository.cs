using School.Domain.Entities;

namespace School.Infrastructure.Repositories
{
	public interface IStudyPlanSubjectRepository : IRepository<StudyPlanSubject>
	{
		void Update(StudyPlanSubject studyPlanSubject);
	}
}
