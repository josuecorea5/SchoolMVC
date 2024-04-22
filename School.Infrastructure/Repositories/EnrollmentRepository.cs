using School.Domain.Entities;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories
{
	public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
	{
		private readonly ApplicationDbContext _context;

		public EnrollmentRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(Enrollment enrollment)
		{
			_context.Enrollments.Update(enrollment);
		}
	}
}
