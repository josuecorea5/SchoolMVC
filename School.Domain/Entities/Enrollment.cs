using School.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Entities
{
	public class Enrollment : BaseAuditableEntity
	{
		[ForeignKey("Student")]
		public int StudentId { get; set; }

		[ForeignKey("Grade")]
		public int GradeId { get; set; }

		public EnrollmentStatus EnrollmentStatus { get; set; }

		public Student Student { get; set; } = null!;

		public Grade Grade { get; set; } = null!;
	}
}
