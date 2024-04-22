using System.Data;

namespace School.Infrastructure.Repositories
{
	public interface IUnitOfWork
	{
		IGradeRepository GradeRepository { get; }
		IStudentRepository StudentRepository { get; }
		ITeacherRepository TeacherRepository { get; }
		ISubjectRepository SubjectRepository { get; }
		IEnrollmentRepository EnrollmentRepository { get; }
		IStudyPlanRepository StudyPlanRepository { get; }
		IStudyPlanSubjectRepository StudyPlanSubjectRepository { get; }
		void Save();
		IDbTransaction BeginTransaction();
	}
}
