using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
	{
		private readonly ApplicationDbContext _context;

		public SubjectRepository(ApplicationDbContext context): base(context)
		{
			_context = context;
		}

		public void Update(Subject subject)
		{
			_context.Subjects.Update(subject);
		}
	}
}
