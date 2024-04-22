using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Entities
{
	public class StudyPlanSubject : BaseAuditableEntity
	{
		[ForeignKey("StudyPlan")]
		public int StudyPlanId { get; set; }

		[ForeignKey("Subject")]
		public int SubjectId { get; set; }

		public StudyPlan StudyPlan { get; set; } = null!;
		public Subject Subject { get; set; } = null!;
	}
}
