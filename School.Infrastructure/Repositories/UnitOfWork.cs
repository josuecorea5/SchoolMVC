using Microsoft.EntityFrameworkCore.Storage;
using School.Infrastructure.Data;
using System.Data;

namespace School.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public IGradeRepository GradeRepository { get; private set; }
		public IStudentRepository StudentRepository { get; private set; }
		public ITeacherRepository TeacherRepository { get; private set; }
		public ISubjectRepository SubjectRepository { get; private set; }
		public IEnrollmentRepository EnrollmentRepository { get; private set; }		
		public IStudyPlanRepository StudyPlanRepository { get; private set; }
		public IStudyPlanSubjectRepository StudyPlanSubjectRepository { get; private set; }

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			GradeRepository = new GradeRepository(context);
			StudentRepository = new StudentRepository(context);
			TeacherRepository = new TeacherRepository(context);
			SubjectRepository = new SubjectRepository(context);
			StudyPlanRepository = new StudyPlanRepository(context);
			EnrollmentRepository = new EnrollmentRepository(context);
			StudyPlanSubjectRepository = new StudyPlanSubjectRepository(context);
		}

        public void Save()
		{
			_context.SaveChanges();
		}

		public IDbTransaction BeginTransaction()
		{
			var transaction = _context.Database.BeginTransaction();

			return transaction.GetDbTransaction();
		}
	}
}
