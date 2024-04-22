using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	public class GradeRepository : Repository<Grade>, IGradeRepository
	{
		private readonly ApplicationDbContext _context;

		public GradeRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Grade grade)
		{
			_context.Grades.Update(grade);
		}
	}
}
