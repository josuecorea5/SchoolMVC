using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Entities
{
	public class StudyPlan : BaseAuditableEntity
	{
		[ForeignKey("Grade")]
		public int GradeId { get; set; }
		public Grade Grade { get; set; } = null!;
		public IEnumerable<StudyPlanSubject>? Subjects { get; set; }
	}
}
