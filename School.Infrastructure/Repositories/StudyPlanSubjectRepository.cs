using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	public class StudyPlanSubjectRepository : Repository<StudyPlanSubject>, IStudyPlanSubjectRepository
	{
		private readonly ApplicationDbContext _context;

		public StudyPlanSubjectRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(StudyPlanSubject studyPlanSubject)
		{
			_context.StudyPlanSubjects.Update(studyPlanSubject);
		}
	}
}
