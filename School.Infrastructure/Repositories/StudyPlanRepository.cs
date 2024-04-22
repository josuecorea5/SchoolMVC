using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	internal class StudyPlanRepository : Repository<StudyPlan>, IStudyPlanRepository
	{
		private readonly ApplicationDbContext _context;

		public StudyPlanRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(StudyPlan entity)
		{
			_context.StudyPlans.Update(entity);
		}
	}
}
